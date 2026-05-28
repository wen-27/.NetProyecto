using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Seeders;

public static class DevelopmentDataSeeder
{
    private const string DefaultPassword = "Admin123*";

    public static async Task SeedDevelopmentDataAsync(this IServiceProvider serviceProvider, IConfiguration configuration)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await using var transaction = await context.Database.BeginTransactionAsync();

        await SeedUsersAsync(context, configuration);
        await SeedInventoryAsync(context);
        await SeedWorkshopServicesAsync(context);

        await transaction.CommitAsync();
    }

    private static async Task SeedUsersAsync(AppDbContext context, IConfiguration configuration)
    {
        var users = new[]
        {
            new SeedUser(
                RoleName: "Admin",
                Email: configuration["SeedUsers:Admin:Email"] ?? "admin@autotaller.com",
                Password: configuration["SeedUsers:Admin:Password"] ?? DefaultPassword,
                DocumentNumber: configuration["SeedUsers:Admin:DocumentNumber"] ?? "ADMIN-0001",
                FirstName: "Admin",
                LastName: "AutoTaller"),
            new SeedUser(
                RoleName: "Mechanic",
                Email: configuration["SeedUsers:Mechanic:Email"] ?? "mecanico@autotaller.com",
                Password: configuration["SeedUsers:Mechanic:Password"] ?? DefaultPassword,
                DocumentNumber: configuration["SeedUsers:Mechanic:DocumentNumber"] ?? "MECH-0001",
                FirstName: "Mecanico",
                LastName: "Principal"),
            new SeedUser(
                RoleName: "Receptionist",
                Email: configuration["SeedUsers:Receptionist:Email"] ?? "recepcion@autotaller.com",
                Password: configuration["SeedUsers:Receptionist:Password"] ?? DefaultPassword,
                DocumentNumber: configuration["SeedUsers:Receptionist:DocumentNumber"] ?? "RECEP-0001",
                FirstName: "Recepcionista",
                LastName: "Principal"),
            new SeedUser(
                RoleName: "Client",
                Email: configuration["SeedUsers:Client:Email"] ?? "cliente@autotaller.com",
                Password: configuration["SeedUsers:Client:Password"] ?? DefaultPassword,
                DocumentNumber: configuration["SeedUsers:Client:DocumentNumber"] ?? "CLIENT-0001",
                FirstName: "Cliente",
                LastName: "Demo"),
            new SeedUser(
                RoleName: "WorkshopChief",
                Email: configuration["SeedUsers:WorkshopChief:Email"] ?? "jefetaller@autotaller.com",
                Password: configuration["SeedUsers:WorkshopChief:Password"] ?? DefaultPassword,
                DocumentNumber: configuration["SeedUsers:WorkshopChief:DocumentNumber"] ?? "CHIEF-WORKSHOP-0001",
                FirstName: "Jefe",
                LastName: "Taller"),
            new SeedUser(
                RoleName: "WarehouseChief",
                Email: configuration["SeedUsers:WarehouseChief:Email"] ?? "jefebodega@autotaller.com",
                Password: configuration["SeedUsers:WarehouseChief:Password"] ?? DefaultPassword,
                DocumentNumber: configuration["SeedUsers:WarehouseChief:DocumentNumber"] ?? "CHIEF-WAREHOUSE-0001",
                FirstName: "Jefe",
                LastName: "Bodega"),
            new SeedUser(
                RoleName: "InventoryManager",
                Email: configuration["SeedUsers:InventoryManager:Email"] ?? "jefealmacen@autotaller.com",
                Password: configuration["SeedUsers:InventoryManager:Password"] ?? DefaultPassword,
                DocumentNumber: configuration["SeedUsers:InventoryManager:DocumentNumber"] ?? "MANAGER-INVENTORY-0001",
                FirstName: "Jefe",
                LastName: "Almacen")
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

        var role = await context.Roles.AsTracking().FirstOrDefaultAsync(x => x.RoleName == seedUser.RoleName);
        if (role is null)
        {
            role = new Role { RoleName = seedUser.RoleName };
            await context.Roles.AddAsync(role);
            await context.SaveChangesAsync();
        }

        var documentType = await context.DocumentTypes.AsTracking().FirstOrDefaultAsync(x => x.Code == "CC");
        if (documentType is null)
        {
            documentType = new DocumentType { Code = "CC", Name = "Cedula de Ciudadania" };
            await context.DocumentTypes.AddAsync(documentType);
            await context.SaveChangesAsync();
        }

        var domain = await context.EmailDomains.AsTracking().FirstOrDefaultAsync(x => x.Domain == emailParts.Domain);
        if (domain is null)
        {
            domain = new EmailDomain { Domain = emailParts.Domain };
            await context.EmailDomains.AddAsync(domain);
            await context.SaveChangesAsync();
        }

        var existingUser = await context.Users
            .AsTracking()
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

        var person = await context.Persons.AsTracking().FirstOrDefaultAsync(x => x.DocumentNumber == seedUser.DocumentNumber);
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
        var personRole = await context.PersonRoles.AsTracking().FirstOrDefaultAsync(x => x.PersonId == personId && x.RoleId == roleId);
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
        var specialty = await context.MechanicSpecialties.AsTracking().FirstOrDefaultAsync(x => x.Name == specialtyName);
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

        var suppliers = new Dictionary<string, int>
        {
            ["AutoPartes Colombia S.A.S."] = await EnsureSupplierAsync(context, "AutoPartes Colombia S.A.S.", "900123456-1", "6071234567", "ventas@autopartes.test"),
            ["Lubricantes del Oriente"] = await EnsureSupplierAsync(context, "Lubricantes del Oriente", "900123456-2", "6077654321", "ventas@lubrioriente.test"),
            ["Repuestos Premium Bucaramanga"] = await EnsureSupplierAsync(context, "Repuestos Premium Bucaramanga", "900123456-3", "6073332211", "ventas@premiumbga.test"),
            ["Distribuidora Nacional de Llantas"] = await EnsureSupplierAsync(context, "Distribuidora Nacional de Llantas", "900123456-4", "6074443322", "ventas@llantasnacional.test"),
            ["Baterías Andinas"] = await EnsureSupplierAsync(context, "Baterías Andinas", "900123456-5", "6075554433", "ventas@bateriasandinas.test")
        };

        var parts = new[]
        {
            new SeedPart("REF-ACE-20W50", "Aceite 20W50", 2, mobilId, 40, 10, 78000m),
            new SeedPart("REF-FIL-ACE-UNI", "Filtro de aceite universal", 1, boschId, 35, 8, 36000m),
            new SeedPart("REF-LLA-R15", "Llanta rin 15", 4, monroeId, 24, 6, 260000m),
            new SeedPart("REF-PAS-FRE-DEL", "Pastillas de freno delanteras", 3, acDelcoId, 30, 8, 145000m),
            new SeedPart("REF-BAT-12V", "Batería 12V", 5, acDelcoId, 18, 4, 390000m),
            new SeedPart("REF-FIL-AIR", "Filtro de aire", 1, densoId, 32, 8, 52000m),
            new SeedPart("REF-BUJ-STD", "Bujías estándar", 5, ngkId, 80, 20, 28000m),
            new SeedPart("REF-LIQ-FRE", "Líquido de frenos", 3, acDelcoId, 28, 8, 42000m),
            new SeedPart("REF-COR-REP", "Correa de repartición", 7, boschId, 16, 4, 165000m),
            new SeedPart("REF-LIM-UNI", "Limpiaparabrisas universal", 8, boschId, 35, 10, 34000m)
        };

        var seededPartIds = new List<int>();
        foreach (var part in parts)
        {
            seededPartIds.Add(await EnsurePartAsync(context, part));
        }

        await EnsureInitialPurchaseAsync(context, suppliers["AutoPartes Colombia S.A.S."], seededPartIds);
        await context.SaveChangesAsync();
    }

    private static async Task SeedWorkshopServicesAsync(AppDbContext context)
    {
        await EnsureWorkshopServiceAsync(context, "Cambio de aceite", "Cambio de aceite del motor y filtro.", "Mantenimiento", 30m, new[] { ("REF-ACE-20W50", 1), ("REF-FIL-ACE-UNI", 1) });
        await EnsureWorkshopServiceAsync(context, "Cambio de llantas", "Cambio de cuatro llantas.", "Llantas", 20m, new[] { ("REF-LLA-R15", 4) });
        await EnsureWorkshopServiceAsync(context, "Revisión de frenos", "Revisión y reemplazo básico de componentes delanteros.", "Frenos", 35m, new[] { ("REF-PAS-FRE-DEL", 1), ("REF-LIQ-FRE", 1) });
        await EnsureWorkshopServiceAsync(context, "Cambio de batería", "Cambio de batería principal.", "Eléctrico", 15m, new[] { ("REF-BAT-12V", 1) });
        await EnsureWorkshopServiceAsync(context, "Cambio de filtro de aire", "Cambio de filtro de aire del motor.", "Mantenimiento", 20m, new[] { ("REF-FIL-AIR", 1) });
        await EnsureWorkshopServiceAsync(context, "Cambio de bujías", "Cambio de bujías estándar.", "Motor", 25m, new[] { ("REF-BUJ-STD", 4) });
        await EnsureWorkshopServiceAsync(context, "Alineación", "Alineación del vehículo.", "Llantas", 100m, Array.Empty<(string Code, int Quantity)>());
        await EnsureWorkshopServiceAsync(context, "Balanceo", "Balanceo de ruedas.", "Llantas", 100m, Array.Empty<(string Code, int Quantity)>());
        await EnsureWorkshopServiceAsync(context, "Diagnóstico general", "Diagnóstico general del vehículo.", "Diagnóstico", 100m, Array.Empty<(string Code, int Quantity)>());
        await EnsureWorkshopServiceAsync(context, "Mantenimiento preventivo", "Mantenimiento preventivo básico.", "Mantenimiento", 30m, new[] { ("REF-ACE-20W50", 1), ("REF-FIL-ACE-UNI", 1), ("REF-FIL-AIR", 1) });
        await context.SaveChangesAsync();
    }

    private static async Task EnsureWorkshopServiceAsync(AppDbContext context, string name, string description, string category, decimal laborPercentage, IReadOnlyCollection<(string Code, int Quantity)> partDefinitions)
    {
        var service = await context.WorkshopServices
            .AsTracking()
            .Include(x => x.Parts)
            .FirstOrDefaultAsync(x => x.Name == name);

        if (service is null)
        {
            service = new WorkshopService { Name = name, CreatedAt = DateTime.UtcNow };
            await context.WorkshopServices.AddAsync(service);
            await context.SaveChangesAsync();
        }

        service.Description = description;
        service.Category = category;
        service.LaborPercentage = laborPercentage;
        service.Status = Domain.Enums.WorkshopServiceStatus.Active;
        service.IsActive = true;

        context.WorkshopServiceParts.RemoveRange(service.Parts);
        service.Parts.Clear();

        decimal subtotal = 0m;
        foreach (var definition in partDefinitions)
        {
            var part = await context.Parts.FirstAsync(x => x.Code == definition.Code);
            var lineTotal = part.UnitPrice * definition.Quantity;
            subtotal += lineTotal;
            service.Parts.Add(new WorkshopServicePart
            {
                WorkshopServiceId = service.Id,
                PartId = part.Id,
                QuantityRequired = definition.Quantity,
                UnitSalePrice = part.UnitPrice,
                LineTotal = lineTotal
            });
        }

        service.PartsSubtotal = subtotal;
        service.LaborAmount = subtotal * laborPercentage / 100m;
        service.FinalPrice = service.PartsSubtotal + service.LaborAmount;
    }

    private static async Task<int> EnsurePartBrandAsync(AppDbContext context, string name)
    {
        var brand = await context.PartBrands.AsTracking().FirstOrDefaultAsync(x => x.Name == name);
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
        var supplier = await context.Suppliers.AsTracking().FirstOrDefaultAsync(x => x.TaxId == taxId);
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
        var part = await context.Parts.AsTracking().FirstOrDefaultAsync(x => x.Code == seed.Code);
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
            .AsTracking()
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
