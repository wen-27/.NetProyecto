using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Seeders;

public static class DevelopmentDataSeeder
{
    private const string DefaultPassword = "Password123*";

    public static async Task SeedDevelopmentDataAsync(this IServiceProvider serviceProvider, IConfiguration configuration)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await using var transaction = await context.Database.BeginTransactionAsync();

        await SeedUsersAsync(context, configuration);
        await SeedInventoryAsync(context);

        await transaction.CommitAsync();
    }

    private static async Task SeedUsersAsync(AppDbContext context, IConfiguration configuration)
    {
        var users = new[]
        {
            new SeedUser(
                RoleName: "Admin",
                Email: configuration["SeedUsers:Admin:Email"] ?? "admin@mail.com",
                Password: configuration["SeedUsers:Admin:Password"] ?? DefaultPassword,
                DocumentNumber: configuration["SeedUsers:Admin:DocumentNumber"] ?? "ADMIN-0001",
                FirstName: "Admin",
                LastName: "AutoTaller"),
            new SeedUser(
                RoleName: "Mechanic",
                Email: configuration["SeedUsers:Mechanic:Email"] ?? "mechanic@mail.com",
                Password: configuration["SeedUsers:Mechanic:Password"] ?? DefaultPassword,
                DocumentNumber: configuration["SeedUsers:Mechanic:DocumentNumber"] ?? "MECH-0001",
                FirstName: "Mecanico",
                LastName: "Principal"),
            new SeedUser(
                RoleName: "Receptionist",
                Email: configuration["SeedUsers:Receptionist:Email"] ?? "receptionist@mail.com",
                Password: configuration["SeedUsers:Receptionist:Password"] ?? DefaultPassword,
                DocumentNumber: configuration["SeedUsers:Receptionist:DocumentNumber"] ?? "RECEP-0001",
                FirstName: "Recepcionista",
                LastName: "Principal"),
            new SeedUser(
                RoleName: "Client",
                Email: configuration["SeedUsers:Client:Email"] ?? "client@mail.com",
                Password: configuration["SeedUsers:Client:Password"] ?? DefaultPassword,
                DocumentNumber: configuration["SeedUsers:Client:DocumentNumber"] ?? "CLIENT-0001",
                FirstName: "Cliente",
                LastName: "Demo")
        };

        foreach (var user in users)
        {
            var personId = await EnsureUserAsync(context, user);

            if (user.RoleName == "Mechanic")
            {
                await EnsureMechanicSpecialtyAsync(context, personId, "GeneralDiagnostics");
                await EnsureMechanicSpecialtyAsync(context, personId, "Engine");
            }
        }

        await context.SaveChangesAsync();
    }

    private static async Task<int> EnsureUserAsync(AppDbContext context, SeedUser seedUser)
    {
        var normalizedEmail = seedUser.Email.Trim().ToLowerInvariant();
        var emailParts = SplitEmail(normalizedEmail)
            ?? throw new InvalidOperationException($"El correo {seedUser.Email} no tiene un formato valido.");

        var role = await context.Roles.FirstOrDefaultAsync(x => x.RoleName == seedUser.RoleName);
        if (role is null)
        {
            role = new Role { RoleName = seedUser.RoleName };
            await context.Roles.AddAsync(role);
            await context.SaveChangesAsync();
        }

        var documentType = await context.DocumentTypes.FirstOrDefaultAsync(x => x.Code == "CC");
        if (documentType is null)
        {
            documentType = new DocumentType { Code = "CC", Name = "Cedula de Ciudadania" };
            await context.DocumentTypes.AddAsync(documentType);
            await context.SaveChangesAsync();
        }

        var domain = await context.EmailDomains.FirstOrDefaultAsync(x => x.Domain == emailParts.Domain);
        if (domain is null)
        {
            domain = new EmailDomain { Domain = emailParts.Domain };
            await context.EmailDomains.AddAsync(domain);
            await context.SaveChangesAsync();
        }

        var existingUser = await context.Users
            .Include(x => x.Person)
            .ThenInclude(x => x.Emails)
            .ThenInclude(x => x.EmailDomain)
            .FirstOrDefaultAsync(x => x.Person.Emails.Any(personEmail =>
                (personEmail.EmailUser + "@" + personEmail.EmailDomain.Domain).ToLower() == normalizedEmail));

        if (existingUser is not null)
        {
            existingUser.IsActive = true;
            existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(seedUser.Password);
            existingUser.Person.FirstName = seedUser.FirstName;
            existingUser.Person.LastName = seedUser.LastName;

            await EnsurePersonRoleAsync(context, existingUser.PersonId, role.Id);
            return existingUser.PersonId;
        }

        var person = await context.Persons.FirstOrDefaultAsync(x => x.DocumentNumber == seedUser.DocumentNumber);
        if (person is null)
        {
            person = new Person
            {
                DocumentTypeId = documentType.Id,
                DocumentNumber = seedUser.DocumentNumber,
                FirstName = seedUser.FirstName,
                LastName = seedUser.LastName,
                CreatedAt = DateTime.UtcNow
            };

            await context.Persons.AddAsync(person);
            await context.SaveChangesAsync();
        }

        var hasEmail = await context.PersonEmails.AnyAsync(x =>
            x.PersonId == person.Id &&
            x.EmailDomainId == domain.Id &&
            x.EmailUser == emailParts.User);

        if (!hasEmail)
        {
            await context.PersonEmails.AddAsync(new PersonEmail
            {
                PersonId = person.Id,
                EmailDomainId = domain.Id,
                EmailUser = emailParts.User,
                IsPrimary = true
            });
        }

        await EnsurePersonRoleAsync(context, person.Id, role.Id);

        var hasUser = await context.Users.AnyAsync(x => x.PersonId == person.Id);
        if (!hasUser)
        {
            await context.Users.AddAsync(new User
            {
                PersonId = person.Id,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(seedUser.Password),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });
        }

        return person.Id;
    }

    private static async Task EnsurePersonRoleAsync(AppDbContext context, int personId, int roleId)
    {
        var personRole = await context.PersonRoles.FirstOrDefaultAsync(x => x.PersonId == personId && x.RoleId == roleId);
        if (personRole is null)
        {
            await context.PersonRoles.AddAsync(new PersonRole
            {
                PersonId = personId,
                RoleId = roleId,
                IsActive = true
            });
            return;
        }

        personRole.IsActive = true;
    }

    private static async Task EnsureMechanicSpecialtyAsync(AppDbContext context, int personId, string specialtyName)
    {
        var specialty = await context.MechanicSpecialties.FirstOrDefaultAsync(x => x.Name == specialtyName);
        if (specialty is null)
        {
            specialty = new MechanicSpecialty { Name = specialtyName };
            await context.MechanicSpecialties.AddAsync(specialty);
            await context.SaveChangesAsync();
        }

        var exists = await context.MechanicSpecialtyAssignments.AnyAsync(x =>
            x.PersonId == personId &&
            x.SpecialtyId == specialty.Id);

        if (!exists)
        {
            await context.MechanicSpecialtyAssignments.AddAsync(new MechanicSpecialtyAssignment
            {
                PersonId = personId,
                SpecialtyId = specialty.Id
            });
        }
    }

    private static async Task SeedInventoryAsync(AppDbContext context)
    {
        var boschId = await EnsurePartBrandAsync(context, "Bosch");
        var acDelcoId = await EnsurePartBrandAsync(context, "ACDelco");
        var mobilId = await EnsurePartBrandAsync(context, "Mobil");
        var ngkId = await EnsurePartBrandAsync(context, "NGK");
        var monroeId = await EnsurePartBrandAsync(context, "Monroe");
        var densoId = await EnsurePartBrandAsync(context, "Denso");

        var supplierId = await EnsureSupplierAsync(
            context,
            name: "AutoParts Colombia",
            taxId: "900123456-1",
            phone: "6071234567",
            email: "ventas@autoparts.test");

        var parts = new[]
        {
            new SeedPart("FIL-AIR-001", "Filtro de aire motor", 1, boschId, 25, 5, 35000m),
            new SeedPart("FIL-OIL-001", "Filtro de aceite", 1, boschId, 30, 5, 28000m),
            new SeedPart("OIL-10W30-001", "Aceite sintetico 10W30 1L", 2, mobilId, 60, 12, 42000m),
            new SeedPart("BRK-PAD-001", "Pastillas de freno delanteras", 3, acDelcoId, 18, 4, 120000m),
            new SeedPart("SUS-SHOCK-001", "Amortiguador delantero", 4, monroeId, 10, 2, 260000m),
            new SeedPart("ELE-SPARK-001", "Bujia iridium", 5, ngkId, 40, 8, 38000m),
            new SeedPart("AC-FILTER-001", "Filtro de cabina aire acondicionado", 6, densoId, 20, 4, 52000m),
            new SeedPart("ENG-BELT-001", "Correa de accesorios", 7, boschId, 15, 3, 85000m)
        };

        var seededPartIds = new List<int>();
        foreach (var part in parts)
        {
            seededPartIds.Add(await EnsurePartAsync(context, part));
        }

        await EnsureInitialPurchaseAsync(context, supplierId, seededPartIds);
        await context.SaveChangesAsync();
    }

    private static async Task<int> EnsurePartBrandAsync(AppDbContext context, string name)
    {
        var brand = await context.PartBrands.FirstOrDefaultAsync(x => x.Name == name);
        if (brand is null)
        {
            brand = new PartBrand { Name = name };
            await context.PartBrands.AddAsync(brand);
            await context.SaveChangesAsync();
        }

        return brand.Id;
    }

    private static async Task<int> EnsureSupplierAsync(AppDbContext context, string name, string taxId, string phone, string email)
    {
        var supplier = await context.Suppliers.FirstOrDefaultAsync(x => x.TaxId == taxId);
        if (supplier is null)
        {
            supplier = new Supplier
            {
                Name = name,
                TaxId = taxId,
                Phone = phone,
                Email = email,
                Status = true
            };
            await context.Suppliers.AddAsync(supplier);
            await context.SaveChangesAsync();
        }
        else
        {
            supplier.Name = name;
            supplier.Phone = phone;
            supplier.Email = email;
            supplier.Status = true;
        }

        return supplier.Id;
    }

    private static async Task<int> EnsurePartAsync(AppDbContext context, SeedPart seed)
    {
        var part = await context.Parts.FirstOrDefaultAsync(x => x.Code == seed.Code);
        if (part is null)
        {
            part = new Part
            {
                Code = seed.Code,
                Description = seed.Description,
                PartCategoryId = seed.PartCategoryId,
                PartBrandId = seed.PartBrandId,
                Stock = seed.Stock,
                MinimumStock = seed.MinimumStock,
                UnitPrice = seed.UnitPrice,
                IsActive = true
            };

            await context.Parts.AddAsync(part);
            await context.SaveChangesAsync();
        }
        else
        {
            part.Description = seed.Description;
            part.PartCategoryId = seed.PartCategoryId;
            part.PartBrandId = seed.PartBrandId;
            part.Stock = Math.Max(part.Stock, seed.Stock);
            part.MinimumStock = seed.MinimumStock;
            part.UnitPrice = seed.UnitPrice;
            part.IsActive = true;
        }

        return part.Id;
    }

    private static async Task EnsureInitialPurchaseAsync(AppDbContext context, int supplierId, IReadOnlyCollection<int> partIds)
    {
        var existingPurchase = await context.PartPurchases
            .Include(x => x.Details)
            .FirstOrDefaultAsync(x => x.SupplierId == supplierId);

        if (existingPurchase is not null)
        {
            return;
        }

        var parts = await context.Parts
            .Where(x => partIds.Contains(x.Id))
            .ToListAsync();

        var purchase = new PartPurchase
        {
            SupplierId = supplierId,
            PurchaseDate = DateTime.UtcNow,
            Total = parts.Sum(x => x.Stock * x.UnitPrice)
        };

        await context.PartPurchases.AddAsync(purchase);
        await context.SaveChangesAsync();

        foreach (var part in parts)
        {
            await context.PartPurchaseDetails.AddAsync(new PartPurchaseDetail
            {
                PartPurchaseId = purchase.Id,
                PartId = part.Id,
                Quantity = part.Stock,
                UnitPrice = part.UnitPrice
            });
        }
    }

    private static (string User, string Domain)? SplitEmail(string email)
    {
        var parts = email.Split('@', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2 || parts[0].Length == 0 || parts[1].Length == 0 || !parts[1].Contains('.'))
        {
            return null;
        }

        return (parts[0], parts[1]);
    }

    private sealed record SeedUser(
        string RoleName,
        string Email,
        string Password,
        string DocumentNumber,
        string FirstName,
        string LastName);

    private sealed record SeedPart(
        string Code,
        string Description,
        int PartCategoryId,
        int? PartBrandId,
        int Stock,
        int MinimumStock,
        decimal UnitPrice);
}
