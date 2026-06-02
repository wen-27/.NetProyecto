using Domain.Entities;
using Domain.Enums;
using Domain.Enums.OrderStatus;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Seeders;

public static class DevelopmentDataSeeder
{
    private const string DefaultPassword = "DevPass123!";

    public static async Task SeedDevelopmentDataAsync(this IServiceProvider serviceProvider, IConfiguration configuration)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Database.MigrateAsync();
        await EnsureMechanicDiagnosticsTableAsync(context);

        await using var transaction = await context.Database.BeginTransactionAsync();

        await SeedUsersAsync(context, configuration);
        await SeedInventoryAsync(context);
        await SeedWorkshopServicesAsync(context);
        await SeedOperationalScenarioAsync(context);
        await EnsureSeedPanelRolesAsync(context);
        await SeedAuditTrailAsync(context);

        await transaction.CommitAsync();
    }

    private static Task EnsureMechanicDiagnosticsTableAsync(AppDbContext context)
    {
        return context.Database.ExecuteSqlRawAsync("""
            CREATE TABLE IF NOT EXISTS `MechanicDiagnostics` (
                `MechanicDiagnosticId` int NOT NULL AUTO_INCREMENT,
                `ServiceOrderId` int NOT NULL,
                `MechanicPersonId` int NOT NULL,
                `WorkshopChiefPersonId` int NULL,
                `Status` int NOT NULL DEFAULT 1,
                `Findings` longtext NOT NULL,
                `RecommendedWork` longtext NOT NULL,
                `WorkshopChiefComment` longtext NULL,
                `SubmittedAt` datetime(6) NOT NULL,
                `ReviewedAt` datetime(6) NULL,
                `CreatedAt` datetime(6) NOT NULL,
                PRIMARY KEY (`MechanicDiagnosticId`),
                KEY `IX_MechanicDiagnostics_MechanicPersonId` (`MechanicPersonId`),
                KEY `IX_MechanicDiagnostics_ServiceOrderId_MechanicPersonId_Status` (`ServiceOrderId`, `MechanicPersonId`, `Status`),
                KEY `IX_MechanicDiagnostics_WorkshopChiefPersonId` (`WorkshopChiefPersonId`),
                CONSTRAINT `FK_MechanicDiagnostics_Persons_MechanicPersonId`
                    FOREIGN KEY (`MechanicPersonId`) REFERENCES `Persons` (`PersonId`) ON DELETE RESTRICT,
                CONSTRAINT `FK_MechanicDiagnostics_Persons_WorkshopChiefPersonId`
                    FOREIGN KEY (`WorkshopChiefPersonId`) REFERENCES `Persons` (`PersonId`) ON DELETE RESTRICT,
                CONSTRAINT `FK_MechanicDiagnostics_ServiceOrders_ServiceOrderId`
                    FOREIGN KEY (`ServiceOrderId`) REFERENCES `ServiceOrders` (`ServiceOrderId`) ON DELETE RESTRICT
            ) CHARACTER SET utf8mb4;
            """);
    }

    private static async Task SeedUsersAsync(AppDbContext context, IConfiguration configuration)
    {
        var users = new[]
        {
            new SeedUser(
                RoleName: "Admin",
                Email: "admin@autotaller.com",
                Password: DefaultPassword,
                DocumentNumber: "ADMIN-0001",
                FirstName: "Admin",
                LastName: "Sistema"),
            new SeedUser(
                RoleName: "Mechanic",
                Email: "mecanico@autotaller.com",
                Password: DefaultPassword,
                DocumentNumber: "MECH-0001",
                FirstName: "Mecanico",
                LastName: "Principal",
                Specialties: new[] { "GeneralDiagnostics", "Engine", "Diagnóstico" }),
            new SeedUser(
                RoleName: "Mechanic",
                Email: "diagnostico@autotaller.com",
                Password: DefaultPassword,
                DocumentNumber: "MECH-DIAG-0001",
                FirstName: "Diego",
                LastName: "Herrera",
                Specialties: new[] { "Diagnóstico", "GeneralDiagnostics" }),
            new SeedUser(
                RoleName: "Mechanic",
                Email: "mantenimiento@autotaller.com",
                Password: DefaultPassword,
                DocumentNumber: "MECH-MANT-0001",
                FirstName: "Luis",
                LastName: "Martinez",
                Specialties: new[] { "Mantenimiento" }),
            new SeedUser(
                RoleName: "Mechanic",
                Email: "electricista@autotaller.com",
                Password: DefaultPassword,
                DocumentNumber: "MECH-ELEC-0001",
                FirstName: "Felipe",
                LastName: "Torres",
                Specialties: new[] { "Electricista", "Electrical" }),
            new SeedUser(
                RoleName: "Mechanic",
                Email: "frenos@autotaller.com",
                Password: DefaultPassword,
                DocumentNumber: "MECH-FREN-0001",
                FirstName: "Camilo",
                LastName: "Vargas",
                Specialties: new[] { "Frenos", "Brakes" }),
            new SeedUser(
                RoleName: "Receptionist",
                Email: "recepcionista@autotaller.com",
                Password: DefaultPassword,
                DocumentNumber: "RECEP-0001",
                FirstName: "Andrea",
                LastName: "Rojas"),
            new SeedUser(
                RoleName: "WorkshopChief",
                Email: "jefe.mecanicos@autotaller.com",
                Password: DefaultPassword,
                DocumentNumber: "CHIEF-WORKSHOP-0001",
                FirstName: "Jorge",
                LastName: "Mendez"),
            new SeedUser(
                RoleName: "WarehouseChief",
                Email: "jefebodega@autotaller.com",
                Password: DefaultPassword,
                DocumentNumber: "CHIEF-WAREHOUSE-0001",
                FirstName: "Jefe",
                LastName: "Bodega"),
            new SeedUser(
                RoleName: "InventoryManager",
                Email: "jefealmacen@autotaller.com",
                Password: DefaultPassword,
                DocumentNumber: "MANAGER-INVENTORY-0001",
                FirstName: "Jefe",
                LastName: "Almacen")
        };

        foreach (var user in users)
        {
            var personId = await EnsureUserAsync(context, user);

            if (user.RoleName == "Mechanic")
            {
                foreach (var specialty in user.Specialties ?? Array.Empty<string>())
                {
                    await EnsureMechanicSpecialtyAsync(context, personId, specialty);
                }
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

        var user = await context.Users.AsTracking().FirstOrDefaultAsync(x => x.PersonId == person.Id);
        if (user is null)
        {
            user = new User
            {
                PersonId = person.Id,
                CreatedAt = DateTime.UtcNow
            };
            await context.Users.AddAsync(user);
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(seedUser.Password);
        user.IsActive = true;

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
        await EnsureWorkshopServiceAsync(context, "Revisión eléctrica", "Revisión del sistema eléctrico, batería, alternador y cableado principal.", "Electricista", 25m, Array.Empty<(string Code, int Quantity)>());
        await EnsureWorkshopServiceAsync(context, "Reparación sistema eléctrico", "Corrección de fallas eléctricas y reemplazo básico de componentes.", "Electricista", 30m, new[] { ("REF-BAT-12V", 1), ("REF-BUJ-STD", 2) });
        await EnsureWorkshopServiceAsync(context, "Cambio de pastillas de freno", "Reemplazo de pastillas de freno delanteras y prueba de frenado.", "Frenos", 25m, new[] { ("REF-PAS-FRE-DEL", 1), ("REF-LIQ-FRE", 1) });
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
        service.LaborAmount = subtotal > 0m ? subtotal * laborPercentage / 100m : laborPercentage * 1000m;
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

    private static async Task SeedOperationalScenarioAsync(AppDbContext context)
    {
        var carlos = await EnsureCustomerAsync(context, new SeedCustomer(
            Email: "carlos.ramirez@test.com",
            Password: DefaultPassword,
            DocumentNumber: "1001001001",
            FirstName: "Carlos",
            MiddleName: "Andres",
            LastName: "Ramirez",
            SecondLastName: "Torres",
            PhoneNumber: "3001112233",
            GenderName: "Masculino",
            BirthDate: new DateOnly(1990, 4, 12),
            AddressText: "Calle 45 # 18-25"));

        var laura = await EnsureCustomerAsync(context, new SeedCustomer(
            Email: "laura.gomez@test.com",
            Password: DefaultPassword,
            DocumentNumber: "1002002002",
            FirstName: "Laura",
            MiddleName: "Marcela",
            LastName: "Gomez",
            SecondLastName: "Rios",
            PhoneNumber: "3004445566",
            GenderName: "Femenino",
            BirthDate: new DateOnly(1993, 9, 22),
            AddressText: "Carrera 27 # 35-40"));

        await EnsurePaymentMethodAsync(context, "Efectivo");
        await EnsurePaymentMethodAsync(context, "Transferencia");
        await EnsurePaymentMethodAsync(context, "Tarjeta");
        await EnsurePaymentMethodAsync(context, "Nequi");
        await EnsurePaymentMethodAsync(context, "Daviplata");

        var admin = await GetRequiredUserByEmailAsync(context, "admin@autotaller.com");
        var receptionist = await GetRequiredUserByEmailAsync(context, "recepcionista@autotaller.com");
        var workshopChief = await GetRequiredUserByEmailAsync(context, "jefe.mecanicos@autotaller.com");
        var diagnosticMechanic = await GetRequiredUserByEmailAsync(context, "diagnostico@autotaller.com");
        var maintenanceMechanic = await GetRequiredUserByEmailAsync(context, "mantenimiento@autotaller.com");
        var electricMechanic = await GetRequiredUserByEmailAsync(context, "electricista@autotaller.com");
        var brakesMechanic = await GetRequiredUserByEmailAsync(context, "frenos@autotaller.com");

        var abc123 = await EnsureVehicleAsync(context, "ABC123", "Toyota", "Corolla", "Sedan", 2020, "Gris", 45000);
        await EnsureCurrentOwnerAsync(context, abc123.Id, carlos.PersonId, new DateTime(2024, 1, 10));

        var def456 = await EnsureVehicleAsync(context, "DEF456", "Chevrolet", "Onix", "Sedan", 2022, "Blanco", 28000);
        await EnsureCurrentOwnerAsync(context, def456.Id, carlos.PersonId, new DateTime(2024, 6, 5));

        var ghi789 = await EnsureVehicleAsync(context, "GHI789", "Mazda", "CX-30", "SUV", 2021, "Rojo", 36000);
        await EnsureClosedOwnerHistoryAsync(context, ghi789.Id, carlos.PersonId, new DateTime(2023, 3, 1), new DateTime(2025, 2, 15));
        await EnsureCurrentOwnerAsync(context, ghi789.Id, laura.PersonId, new DateTime(2025, 2, 15));

        var order1 = await EnsureServiceOrderAsync(
            context,
            "SEED-OT-CARLOS-DIAG",
            abc123.Id,
            ServiceOrderStatus.InProgress,
            DateTime.UtcNow.AddDays(-8),
            "Vehiculo ingresa por ruido extrano en motor y revision general.",
            admin.Id);
        await EnsureOrderServiceAsync(context, order1.Id, "Diagnóstico general", "Diagnostics", OrderServiceStatus.InProgress, diagnosticMechanic.PersonId, "Revision inicial por testigo de motor.");
        await EnsureOrderServiceAsync(context, order1.Id, "Revisión de frenos", "Mechanical Repair", OrderServiceStatus.InProgress, brakesMechanic.PersonId, "Revision de frenos por ruido durante frenado.");

        var order2 = await EnsureServiceOrderAsync(
            context,
            "SEED-OT-CARLOS-MANT",
            def456.Id,
            ServiceOrderStatus.InProgress,
            DateTime.UtcNow.AddDays(-5),
            "Mantenimiento preventivo y cambio de aceite para Chevrolet Onix DEF456.",
            admin.Id);
        await EnsureOrderServiceAsync(context, order2.Id, "Mantenimiento preventivo", "Preventive Maintenance", OrderServiceStatus.InProgress, maintenanceMechanic.PersonId, "Mantenimiento preventivo en proceso.");
        await EnsureOrderServiceAsync(context, order2.Id, "Cambio de aceite", "Preventive Maintenance", OrderServiceStatus.InProgress, maintenanceMechanic.PersonId, "Cambio de aceite y filtro en proceso.");

        var order3 = await EnsureServiceOrderAsync(
            context,
            "SEED-OT-LAURA-FRENOS",
            ghi789.Id,
            ServiceOrderStatus.ReadyForDelivery,
            DateTime.UtcNow.AddDays(-12),
            "Revision electrica y frenos para Mazda CX-30 GHI789.",
            admin.Id);
        await EnsureOrderServiceAsync(context, order3.Id, "Revisión eléctrica", "Electrical", OrderServiceStatus.Completed, electricMechanic.PersonId, "Revision electrica completada.");
        await EnsureOrderServiceAsync(context, order3.Id, "Revisión de frenos", "Mechanical Repair", OrderServiceStatus.Completed, brakesMechanic.PersonId, "Revision de frenos completada.");

        var order4 = await EnsureServiceOrderAsync(
            context,
            "SEED-OT-LAURA-CLIENT-APPROVAL",
            ghi789.Id,
            ServiceOrderStatus.PendingClientApproval,
            DateTime.UtcNow.AddDays(-3),
            "Orden completada con trabajos adicionales pendientes de aprobacion del cliente para Mazda CX-30 GHI789.",
            admin.Id);
        await EnsureOrderServiceAsync(context, order4.Id, "Mantenimiento preventivo", "Preventive Maintenance", OrderServiceStatus.Completed, maintenanceMechanic.PersonId, "Mantenimiento preventivo completado y listo para validacion del cliente.");
        await EnsureOrderServiceAsync(context, order4.Id, "Cambio de pastillas de freno", "Mechanical Repair", OrderServiceStatus.Completed, brakesMechanic.PersonId, "Cambio de pastillas recomendado tras prueba de frenado.");

        var order5 = await EnsureServiceOrderAsync(
            context,
            "SEED-OT-CARLOS-CLIENT-APPROVAL",
            def456.Id,
            ServiceOrderStatus.PendingClientApproval,
            DateTime.UtcNow.AddDays(-2),
            "Orden completa con solicitudes adicionales pendientes de aprobacion del cliente Carlos para Chevrolet Onix DEF456.",
            admin.Id);
        await EnsureOrderServiceAsync(context, order5.Id, "Cambio de aceite", "Preventive Maintenance", OrderServiceStatus.Completed, maintenanceMechanic.PersonId, "Cambio de aceite completado; requiere aprobacion del cliente para trabajo adicional.");
        await EnsureOrderServiceAsync(context, order5.Id, "Cambio de filtro de aire", "Preventive Maintenance", OrderServiceStatus.Completed, maintenanceMechanic.PersonId, "Cambio de filtro sugerido por mantenimiento preventivo.");

        var chiefDiagnosticOrder1 = await EnsureServiceOrderAsync(
            context,
            "SEED-CHIEF-ORDER-DIAG-CARLOS",
            abc123.Id,
            ServiceOrderStatus.Created,
            DateTime.UtcNow.AddDays(-1),
            "Orden creada por Jefe de Taller para que diagnostico revise ruido en motor de Toyota Corolla ABC123.",
            workshopChief.Id);

        var chiefDiagnosticOrder2 = await EnsureServiceOrderAsync(
            context,
            "SEED-CHIEF-ORDER-DIAG-LAURA",
            ghi789.Id,
            ServiceOrderStatus.Created,
            DateTime.UtcNow.AddHours(-10),
            "Orden creada por Jefe de Taller para diagnostico preventivo de Mazda CX-30 GHI789.",
            workshopChief.Id);

        await RefreshServiceOrderTotalAsync(context, order1.Id);
        await RefreshServiceOrderTotalAsync(context, order2.Id);
        await RefreshServiceOrderTotalAsync(context, order3.Id);
        await RefreshServiceOrderTotalAsync(context, order4.Id);
        await RefreshServiceOrderTotalAsync(context, order5.Id);
        await RefreshServiceOrderTotalAsync(context, chiefDiagnosticOrder1.Id);
        await RefreshServiceOrderTotalAsync(context, chiefDiagnosticOrder2.Id);

        await EnsureAdditionalServiceRequestAsync(
            context,
            order1.Id,
            diagnosticMechanic.PersonId,
            carlos.PersonId,
            null,
            AdditionalRequestStatus.PendingWorkshopChiefApproval,
            AdditionalRequestType.Service,
            "Reparación sistema eléctrico",
            null,
            null,
            "SEED-REQ-CHIEF-ELECTRICAL",
            "Durante el diagnostico se detectan variaciones de voltaje en alternador y cableado principal. Se solicita aprobacion tecnica para reparar sistema electrico.",
            null,
            null);

        await EnsureAdditionalServiceRequestAsync(
            context,
            order2.Id,
            maintenanceMechanic.PersonId,
            carlos.PersonId,
            null,
            AdditionalRequestStatus.PendingWorkshopChiefApproval,
            AdditionalRequestType.Part,
            null,
            "REF-FIL-AIR",
            1,
            "SEED-REQ-CHIEF-AIR-FILTER",
            "El filtro de aire presenta saturacion alta y afecta el flujo de admision. Se solicita autorizacion para reemplazarlo.",
            null,
            null);

        await EnsureAdditionalServiceRequestAsync(
            context,
            order4.Id,
            brakesMechanic.PersonId,
            laura.PersonId,
            workshopChief.PersonId,
            AdditionalRequestStatus.PendingClientApproval,
            AdditionalRequestType.ServiceWithParts,
            "Cambio de pastillas de freno",
            "REF-PAS-FRE-DEL",
            1,
            "SEED-REQ-CLIENT-BRAKES",
            "La prueba de ruta confirma vibracion al frenar y desgaste en pastillas delanteras. Se requiere cambio para entregar el vehiculo seguro.",
            "Aprobado tecnicamente. Enviar al cliente para autorizacion.",
            null);

        await EnsureAdditionalServiceRequestAsync(
            context,
            order4.Id,
            electricMechanic.PersonId,
            laura.PersonId,
            workshopChief.PersonId,
            AdditionalRequestStatus.PendingClientApproval,
            AdditionalRequestType.Service,
            "Revisión eléctrica",
            null,
            null,
            "SEED-REQ-LAURA-ELECTRICAL",
            "Se recomienda una revision electrica complementaria por variacion intermitente en luces del tablero.",
            "Aprobado por jefe de taller. Solicitar autorizacion de Laura antes de continuar.",
            null);

        await EnsureAdditionalServiceRequestAsync(
            context,
            order4.Id,
            maintenanceMechanic.PersonId,
            laura.PersonId,
            workshopChief.PersonId,
            AdditionalRequestStatus.ApprovedByClient,
            AdditionalRequestType.Part,
            null,
            "REF-FIL-AIR",
            1,
            "SEED-MSG-LAURA-APPROVED-FILTER",
            "Mensaje para cliente: filtro de aire recomendado por mantenimiento preventivo.",
            "Aprobado por jefe de taller. El repuesto mejora el flujo de admision.",
            "Aprobado por Laura para continuar con el mantenimiento.");

        await EnsureAdditionalServiceRequestAsync(
            context,
            order5.Id,
            maintenanceMechanic.PersonId,
            carlos.PersonId,
            workshopChief.PersonId,
            AdditionalRequestStatus.PendingClientApproval,
            AdditionalRequestType.ServiceWithParts,
            "Cambio de filtro de aire",
            "REF-FIL-AIR",
            1,
            "SEED-REQ-CARLOS-AIR-FILTER",
            "El filtro de aire del Onix esta saturado y afecta el rendimiento. Se solicita aprobacion del cliente para reemplazo.",
            "Aprobado tecnicamente por jefe de taller. Enviar a Carlos para aprobacion.",
            null);

        await EnsureAdditionalServiceRequestAsync(
            context,
            order5.Id,
            brakesMechanic.PersonId,
            carlos.PersonId,
            workshopChief.PersonId,
            AdditionalRequestStatus.PendingClientApproval,
            AdditionalRequestType.Part,
            null,
            "REF-LIQ-FRE",
            1,
            "SEED-REQ-CARLOS-BRAKE-FLUID",
            "Nivel y color del liquido de frenos fuera de rango recomendado. Se solicita autorizacion para cambio.",
            "Aprobado por jefe de taller. Pendiente decision del cliente.",
            null);

        await EnsureAdditionalServiceRequestAsync(
            context,
            order5.Id,
            diagnosticMechanic.PersonId,
            carlos.PersonId,
            workshopChief.PersonId,
            AdditionalRequestStatus.RejectedByClient,
            AdditionalRequestType.Service,
            "Reparación sistema eléctrico",
            null,
            null,
            "SEED-MSG-CARLOS-REJECTED-ELECTRICAL",
            "Mensaje para cliente: se sugirio reparacion electrica preventiva por lectura de voltaje irregular.",
            "Aprobado tecnicamente, pero requiere autorizacion del cliente.",
            "Carlos rechazo esta solicitud por ahora.");

        await EnsureAdditionalServiceRequestAsync(
            context,
            order1.Id,
            diagnosticMechanic.PersonId,
            carlos.PersonId,
            null,
            AdditionalRequestStatus.PendingWorkshopChiefApproval,
            AdditionalRequestType.Service,
            "Diagnóstico general",
            null,
            null,
            "SEED-DIAG-MYREQ-PENDING-SCAN",
            "Solicitud de diagnostico: se requiere escaneo avanzado por codigo intermitente en sensor de oxigeno.",
            null,
            null);

        await EnsureAdditionalServiceRequestAsync(
            context,
            order2.Id,
            diagnosticMechanic.PersonId,
            carlos.PersonId,
            workshopChief.PersonId,
            AdditionalRequestStatus.PendingClientApproval,
            AdditionalRequestType.ServiceWithParts,
            "Reparación sistema eléctrico",
            "REF-BUJ-STD",
            2,
            "SEED-DIAG-MYREQ-CLIENT-ELECTRICAL",
            "Solicitud de diagnostico: lectura electrica irregular recomienda ajuste de cableado y reemplazo de bujias.",
            "Aprobado por Jefe de Taller. Enviar a Carlos para autorizacion.",
            null);

        await EnsureAdditionalServiceRequestAsync(
            context,
            order3.Id,
            diagnosticMechanic.PersonId,
            laura.PersonId,
            workshopChief.PersonId,
            AdditionalRequestStatus.RejectedByWorkshopChief,
            AdditionalRequestType.Service,
            "Revisión eléctrica",
            null,
            null,
            "SEED-DIAG-MYREQ-REJECTED-ELECTRICAL",
            "Solicitud de diagnostico: prueba adicional electrica completa para descartar consumo parasitario.",
            "Rechazado por Jefe de Taller: con la revision actual es suficiente.",
            null);

        await EnsureMechanicDiagnosticAsync(
            context,
            order1.Id,
            diagnosticMechanic.PersonId,
            null,
            MechanicDiagnosticStatus.PendingWorkshopChiefApproval,
            "SEED-DIAG-PENDING",
            "Se detecta ruido intermitente y testigo de motor activo.",
            "Realizar escaneo y prueba de sensores antes de aprobar trabajos adicionales.",
            null);

        await EnsureMechanicDiagnosticAsync(
            context,
            order2.Id,
            diagnosticMechanic.PersonId,
            workshopChief.PersonId,
            MechanicDiagnosticStatus.Approved,
            "SEED-DIAG-APPROVED",
            "El sistema electrico requiere ajuste preventivo y cambio de componentes menores.",
            "Aprobar revision electrica y mantenimiento preventivo.",
            "Aprobado para continuar con los trabajos.");

        await EnsureMechanicDiagnosticAsync(
            context,
            order3.Id,
            diagnosticMechanic.PersonId,
            workshopChief.PersonId,
            MechanicDiagnosticStatus.Rejected,
            "SEED-DIAG-REJECTED",
            "Se propuso reemplazo completo del sistema de frenos.",
            "Reemplazo completo no requerido para el estado actual.",
            "Rechazado: solo procede cambio de pastillas y prueba de frenado.");

        var invoice2 = await EnsureInvoiceAsync(context, order2.Id, "Issued", DateTime.UtcNow.AddDays(-3));
        await EnsureInvoiceDetailAsync(context, invoice2.Id, "Mantenimiento preventivo y cambio de aceite", 1, order2.EstimatedTotal);

        var invoice3 = await EnsureInvoiceAsync(context, order3.Id, "Paid", DateTime.UtcNow.AddDays(-9));
        await EnsureInvoiceDetailAsync(context, invoice3.Id, "Revision electrica y frenos", 1, order3.EstimatedTotal);

        await EnsurePaymentAsync(context, invoice2.Id, carlos.PersonId, "Transferencia", "PendingReceptionVerification", order2.EstimatedTotal, "SEED-PAY-PENDING-CARLOS", DateTime.UtcNow.AddDays(-2), null, null);
        await EnsurePaymentAsync(context, invoice2.Id, carlos.PersonId, "Transferencia", "Rejected", order2.EstimatedTotal, "SEED-PAY-REJECTED-CARLOS", DateTime.UtcNow.AddDays(-4), receptionist.PersonId, "Comprobante ilegible.");
        await EnsurePaymentAsync(context, invoice3.Id, laura.PersonId, "Tarjeta", "Approved", order3.EstimatedTotal, "SEED-PAY-APPROVED-LAURA", DateTime.UtcNow.AddDays(-8), receptionist.PersonId, null);

        order2.OrderStatusId = (int)ServiceOrderStatus.PaymentUnderReview;
        order3.DeliveryDate ??= DateTime.UtcNow.AddDays(1);

        await context.SaveChangesAsync();
    }

    private static async Task<SeedPersonResult> EnsureCustomerAsync(AppDbContext context, SeedCustomer seed)
    {
        var normalizedEmail = seed.Email.Trim().ToLowerInvariant();
        var emailParts = SplitEmail(normalizedEmail)
            ?? throw new InvalidOperationException($"El correo {seed.Email} no tiene un formato valido.");

        var clientRole = await EnsureRoleAsync(context, "Client");
        var documentType = await EnsureDocumentTypeAsync(context, "CC", "Cedula de Ciudadania");
        var country = await EnsureCountryAsync(context, "Colombia", "+57");
        var domain = await EnsureEmailDomainAsync(context, emailParts.Domain);
        var gender = await EnsureGenderAsync(context, seed.GenderName);
        var address = await EnsureSeedAddressAsync(context, country.Id, seed.AddressText);

        var existingEmail = await context.PersonEmails
            .AsTracking()
            .Include(x => x.Person)
            .FirstOrDefaultAsync(x => x.EmailDomainId == domain.Id && x.EmailUser == emailParts.User);

        var person = await context.Persons.AsTracking().FirstOrDefaultAsync(x => x.DocumentNumber == seed.DocumentNumber);
        if (person is null && existingEmail is not null)
        {
            person = existingEmail.Person;
        }

        if (person is null)
        {
            person = new Person
            {
                DocumentTypeId = documentType.Id,
                DocumentNumber = seed.DocumentNumber,
                CreatedAt = DateTime.UtcNow
            };
            await context.Persons.AddAsync(person);
            await context.SaveChangesAsync();
        }

        person.FirstName = seed.FirstName;
        person.MiddleName = seed.MiddleName;
        person.LastName = seed.LastName;
        person.SecondLastName = seed.SecondLastName;
        person.DocumentTypeId = documentType.Id;
        person.DocumentNumber = seed.DocumentNumber;
        person.GenderId = gender.Id;
        person.BirthDate = seed.BirthDate;
        person.AddressId = address.Id;
        person.IsActive = true;

        if (existingEmail is null)
        {
            await context.PersonEmails.AddAsync(new PersonEmail
            {
                PersonId = person.Id,
                EmailDomainId = domain.Id,
                EmailUser = emailParts.User,
                IsPrimary = true
            });
        }
        else
        {
            existingEmail.PersonId = person.Id;
            existingEmail.IsPrimary = true;
        }

        var phone = await context.PersonPhones.AsTracking().FirstOrDefaultAsync(x => x.CountryId == country.Id && x.PhoneNumber == seed.PhoneNumber);
        if (phone is null)
        {
            await context.PersonPhones.AddAsync(new PersonPhone
            {
                PersonId = person.Id,
                CountryId = country.Id,
                PhoneNumber = seed.PhoneNumber,
                IsPrimary = true
            });
        }
        else if (phone.PersonId == person.Id)
        {
            phone.IsPrimary = true;
        }
        else
        {
            phone.PersonId = person.Id;
            phone.IsPrimary = true;
        }

        await EnsurePersonRoleAsync(context, person.Id, clientRole.Id);

        var user = await context.Users.AsTracking().FirstOrDefaultAsync(x => x.PersonId == person.Id);
        if (user is null)
        {
            user = new User
            {
                PersonId = person.Id,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(seed.Password),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }
        else
        {
            user.IsActive = true;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(seed.Password);
        }

        return new SeedPersonResult(person.Id, user.Id);
    }

    private static async Task EnsureSeedPanelRolesAsync(AppDbContext context)
    {
        var panelRoles = new[]
        {
            "Admin",
            "Receptionist",
            "Mechanic",
            "WorkshopChief",
            "WarehouseChief",
            "InventoryManager",
            "Client"
        };

        var assignments = new (string Email, string[] Roles)[]
        {
            ("admin@autotaller.com", new[] { "Admin" }),
            ("recepcionista@autotaller.com", new[] { "Receptionist" }),
            ("jefe.mecanicos@autotaller.com", new[] { "WorkshopChief" }),
            ("jefebodega@autotaller.com", new[] { "WarehouseChief" }),
            ("jefealmacen@autotaller.com", new[] { "InventoryManager" }),
            ("mecanico@autotaller.com", new[] { "Mechanic" }),
            ("diagnostico@autotaller.com", new[] { "Mechanic" }),
            ("mantenimiento@autotaller.com", new[] { "Mechanic" }),
            ("electricista@autotaller.com", new[] { "Mechanic" }),
            ("frenos@autotaller.com", new[] { "Mechanic" }),
            ("carlos.ramirez@test.com", new[] { "Client" }),
            ("laura.gomez@test.com", new[] { "Client" })
        };

        foreach (var roleName in panelRoles)
        {
            await EnsureRoleAsync(context, roleName);
        }

        foreach (var assignment in assignments)
        {
            var normalizedEmail = assignment.Email.Trim().ToLowerInvariant();
            var emailParts = SplitEmail(normalizedEmail);
            if (emailParts is null)
            {
                continue;
            }

            var domain = await EnsureEmailDomainAsync(context, emailParts.Value.Domain);
            var personEmail = await context.PersonEmails
                .AsTracking()
                .FirstOrDefaultAsync(x => x.EmailDomainId == domain.Id && x.EmailUser == emailParts.Value.User);

            if (personEmail is null)
            {
                continue;
            }

            var person = await context.Persons.AsTracking().FirstOrDefaultAsync(x => x.Id == personEmail.PersonId);
            if (person is null)
            {
                continue;
            }

            person.IsActive = true;

            var personRoles = await context.PersonRoles
                .AsTracking()
                .Include(x => x.Role)
                .Where(x => x.PersonId == person.Id)
                .ToListAsync();

            foreach (var personRole in personRoles.Where(x => panelRoles.Contains(x.Role.RoleName)))
            {
                personRole.IsActive = assignment.Roles.Contains(personRole.Role.RoleName);
            }

            foreach (var roleName in assignment.Roles)
            {
                var role = await EnsureRoleAsync(context, roleName);
                var personRole = personRoles.FirstOrDefault(x => x.RoleId == role.Id);
                if (personRole is null)
                {
                    await context.PersonRoles.AddAsync(new PersonRole
                    {
                        PersonId = person.Id,
                        RoleId = role.Id,
                        IsActive = true
                    });
                }
                else
                {
                    personRole.IsActive = true;
                }
            }

            var user = await context.Users.AsTracking().FirstOrDefaultAsync(x => x.PersonId == person.Id);
            if (user is null)
            {
                user = new User
                {
                    PersonId = person.Id,
                    CreatedAt = DateTime.UtcNow
                };
                await context.Users.AddAsync(user);
            }

            user.IsActive = true;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(DefaultPassword);
        }

        await context.SaveChangesAsync();
    }

    private static async Task<Role> EnsureRoleAsync(AppDbContext context, string roleName)
    {
        var role = await context.Roles.AsTracking().FirstOrDefaultAsync(x => x.RoleName == roleName);
        if (role is not null)
        {
            return role;
        }

        role = new Role { RoleName = roleName };
        await context.Roles.AddAsync(role);
        await context.SaveChangesAsync();
        return role;
    }

    private static async Task<DocumentType> EnsureDocumentTypeAsync(AppDbContext context, string code, string name)
    {
        var documentType = await context.DocumentTypes.AsTracking().FirstOrDefaultAsync(x => x.Code == code);
        if (documentType is not null)
        {
            return documentType;
        }

        documentType = new DocumentType { Code = code, Name = name };
        await context.DocumentTypes.AddAsync(documentType);
        await context.SaveChangesAsync();
        return documentType;
    }

    private static async Task<Country> EnsureCountryAsync(AppDbContext context, string name, string phoneCode)
    {
        var country = await context.Countries.AsTracking().FirstOrDefaultAsync(x => x.Name == name);
        if (country is not null)
        {
            return country;
        }

        country = new Country { Name = name, PhoneCode = phoneCode };
        await context.Countries.AddAsync(country);
        await context.SaveChangesAsync();
        return country;
    }

    private static async Task<Gender> EnsureGenderAsync(AppDbContext context, string name)
    {
        var gender = await context.Genders.AsTracking().FirstOrDefaultAsync(x => x.Name == name);
        if (gender is not null)
        {
            return gender;
        }

        gender = new Gender { Name = name };
        await context.Genders.AddAsync(gender);
        await context.SaveChangesAsync();
        return gender;
    }

    private static async Task<Address> EnsureSeedAddressAsync(AppDbContext context, int countryId, string addressText)
    {
        var department = await context.Departments.AsTracking().FirstOrDefaultAsync(x => x.CountryId == countryId && x.Name == "Santander");
        if (department is null)
        {
            department = new Department { CountryId = countryId, Name = "Santander" };
            await context.Departments.AddAsync(department);
            await context.SaveChangesAsync();
        }

        var city = await context.Cities.AsTracking().FirstOrDefaultAsync(x => x.DepartmentId == department.Id && x.Name == "Bucaramanga");
        if (city is null)
        {
            city = new City { DepartmentId = department.Id, Name = "Bucaramanga" };
            await context.Cities.AddAsync(city);
            await context.SaveChangesAsync();
        }

        var neighborhood = await context.Neighborhoods.AsTracking().FirstOrDefaultAsync(x => x.CityId == city.Id && x.Name == "Centro");
        if (neighborhood is null)
        {
            neighborhood = new Neighborhood { CityId = city.Id, Name = "Centro" };
            await context.Neighborhoods.AddAsync(neighborhood);
            await context.SaveChangesAsync();
        }

        var streetTypeName = addressText.StartsWith("Carrera", StringComparison.OrdinalIgnoreCase) ? "Carrera" : "Calle";
        var streetType = await context.StreetTypes.AsTracking().FirstOrDefaultAsync(x => x.Name == streetTypeName);
        if (streetType is null)
        {
            streetType = new StreetType { Name = streetTypeName };
            await context.StreetTypes.AddAsync(streetType);
            await context.SaveChangesAsync();
        }

        var address = await context.Addresses.AsTracking().FirstOrDefaultAsync(x =>
            x.NeighborhoodId == neighborhood.Id &&
            x.StreetTypeId == streetType.Id &&
            x.Complement == addressText);
        if (address is not null)
        {
            return address;
        }

        address = new Address
        {
            NeighborhoodId = neighborhood.Id,
            StreetTypeId = streetType.Id,
            Complement = addressText
        };
        await context.Addresses.AddAsync(address);
        await context.SaveChangesAsync();
        return address;
    }

    private static async Task<EmailDomain> EnsureEmailDomainAsync(AppDbContext context, string domainName)
    {
        var domain = await context.EmailDomains.AsTracking().FirstOrDefaultAsync(x => x.Domain == domainName);
        if (domain is not null)
        {
            return domain;
        }

        domain = new EmailDomain { Domain = domainName };
        await context.EmailDomains.AddAsync(domain);
        await context.SaveChangesAsync();
        return domain;
    }

    private static async Task<Vehicle> EnsureVehicleAsync(AppDbContext context, string vin, string brandName, string modelName, string typeName, int year, string color, int mileage)
    {
        var model = await EnsureVehicleModelAsync(context, brandName, modelName);
        var type = await EnsureVehicleTypeAsync(context, typeName);
        var vehicle = await context.Vehicles.AsTracking().FirstOrDefaultAsync(x => x.Vin == vin);

        if (vehicle is null)
        {
            vehicle = new Vehicle { Vin = vin };
            await context.Vehicles.AddAsync(vehicle);
        }

        vehicle.ModelId = model.Id;
        vehicle.VehicleTypeId = type.Id;
        vehicle.Year = year;
        vehicle.Color = color;
        vehicle.Mileage = mileage;
        vehicle.IsActive = true;
        await context.SaveChangesAsync();
        return vehicle;
    }

    private static async Task<VehicleModel> EnsureVehicleModelAsync(AppDbContext context, string brandName, string modelName)
    {
        var brand = await context.VehicleBrands.AsTracking().FirstOrDefaultAsync(x => x.BrandName == brandName);
        if (brand is null)
        {
            brand = new VehicleBrand { BrandName = brandName };
            await context.VehicleBrands.AddAsync(brand);
            await context.SaveChangesAsync();
        }

        var model = await context.VehicleModels.AsTracking().FirstOrDefaultAsync(x => x.BrandId == brand.Id && x.ModelName == modelName);
        if (model is not null)
        {
            return model;
        }

        model = new VehicleModel { BrandId = brand.Id, ModelName = modelName };
        await context.VehicleModels.AddAsync(model);
        await context.SaveChangesAsync();
        return model;
    }

    private static async Task<VehicleType> EnsureVehicleTypeAsync(AppDbContext context, string typeName)
    {
        var type = await context.VehicleTypes.AsTracking().FirstOrDefaultAsync(x => x.Name == typeName);
        if (type is not null)
        {
            return type;
        }

        type = new VehicleType { Name = typeName };
        await context.VehicleTypes.AddAsync(type);
        await context.SaveChangesAsync();
        return type;
    }

    private static async Task EnsureClosedOwnerHistoryAsync(AppDbContext context, int vehicleId, int personId, DateTime startDate, DateTime endDate)
    {
        var existing = await context.VehicleOwnerHistory
            .AsTracking()
            .FirstOrDefaultAsync(x => x.VehicleId == vehicleId && x.PersonId == personId && x.StartDate == startDate);

        if (existing is null)
        {
            await context.VehicleOwnerHistory.AddAsync(new VehicleOwnerHistory
            {
                VehicleId = vehicleId,
                PersonId = personId,
                StartDate = startDate,
                EndDate = endDate
            });
            return;
        }

        existing.EndDate = endDate;
    }

    private static async Task EnsureCurrentOwnerAsync(AppDbContext context, int vehicleId, int personId, DateTime startDate)
    {
        var activeOwners = await context.VehicleOwnerHistory
            .AsTracking()
            .Where(x => x.VehicleId == vehicleId && x.EndDate == null)
            .ToListAsync();

        foreach (var owner in activeOwners.Where(x => x.PersonId != personId))
        {
            owner.EndDate = startDate;
        }

        var current = activeOwners.FirstOrDefault(x => x.PersonId == personId);
        if (current is not null)
        {
            current.StartDate = startDate;
            return;
        }

        await context.VehicleOwnerHistory.AddAsync(new VehicleOwnerHistory
        {
            VehicleId = vehicleId,
            PersonId = personId,
            StartDate = startDate
        });
    }

    private static async Task<ServiceOrder> EnsureServiceOrderAsync(AppDbContext context, string seedCode, int vehicleId, ServiceOrderStatus status, DateTime entryDate, string description, int userId)
    {
        var order = await context.ServiceOrders
            .AsTracking()
            .FirstOrDefaultAsync(x => x.GeneralDescription != null && x.GeneralDescription.Contains(seedCode));

        var fullDescription = $"{description} [{seedCode}]";
        if (order is null)
        {
            order = new ServiceOrder
            {
                VehicleId = vehicleId,
                OrderStatusId = (int)status,
                EntryDate = entryDate,
                EstimatedTotal = 0m,
                GeneralDescription = fullDescription,
                CreatedAt = entryDate
            };
            await context.ServiceOrders.AddAsync(order);
        }

        order.VehicleId = vehicleId;
        order.OrderStatusId = (int)status;
        order.GeneralDescription = fullDescription;
        order.EstimatedDeliveryDate ??= entryDate.AddDays(5);
        await context.SaveChangesAsync();

        await EnsureOrderStatusHistoryAsync(context, order.Id, null, (int)status, userId, $"Seeder: {seedCode}");
        return order;
    }

    private static async Task EnsureOrderStatusHistoryAsync(AppDbContext context, int serviceOrderId, int? previousStatusId, int newStatusId, int userId, string observation)
    {
        var exists = await context.OrderStatusHistory.AnyAsync(x =>
            x.ServiceOrderId == serviceOrderId &&
            x.NewOrderStatusId == newStatusId &&
            x.Observation == observation);

        if (exists)
        {
            return;
        }

        await context.OrderStatusHistory.AddAsync(new OrderStatusHistory
        {
            ServiceOrderId = serviceOrderId,
            PreviousOrderStatusId = previousStatusId,
            NewOrderStatusId = newStatusId,
            UserId = userId,
            ChangeDate = DateTime.UtcNow,
            Observation = observation
        });
    }

    private static async Task<OrderService> EnsureOrderServiceAsync(AppDbContext context, int serviceOrderId, string workshopServiceName, string serviceTypeName, OrderServiceStatus status, int mechanicPersonId, string description)
    {
        var workshopService = await context.WorkshopServices.FirstAsync(x => x.Name == workshopServiceName);
        var serviceType = await context.ServiceTypes.FirstAsync(x => x.Name == serviceTypeName);
        var orderService = await context.OrderServices
            .AsTracking()
            .FirstOrDefaultAsync(x => x.ServiceOrderId == serviceOrderId && x.WorkshopServiceId == workshopService.Id);

        if (orderService is null)
        {
            orderService = new OrderService
            {
                ServiceOrderId = serviceOrderId,
                WorkshopServiceId = workshopService.Id,
                ServiceTypeId = serviceType.Id
            };
            await context.OrderServices.AddAsync(orderService);
        }

        orderService.ServiceTypeId = serviceType.Id;
        orderService.Description = description;
        orderService.LaborCost = workshopService.LaborAmount;
        orderService.Price = workshopService.FinalPrice;
        orderService.Status = status;
        orderService.CustomerApproved = status is OrderServiceStatus.Approved or OrderServiceStatus.InProgress or OrderServiceStatus.Completed;
        orderService.ApprovalDate = orderService.CustomerApproved == true ? DateTime.UtcNow.AddDays(-3) : null;
        await context.SaveChangesAsync();

        var specialtyName = SpecialtyForWorkshopCategory(workshopService.Category);
        var specialty = await context.MechanicSpecialties.FirstAsync(x => x.Name == specialtyName);
        var assignmentExists = await context.MechanicAssignments.AnyAsync(x => x.OrderServiceId == orderService.Id && x.MechanicPersonId == mechanicPersonId);
        if (!assignmentExists)
        {
            await context.MechanicAssignments.AddAsync(new MechanicAssignment
            {
                OrderServiceId = orderService.Id,
                MechanicPersonId = mechanicPersonId,
                SpecialtyId = specialty.Id
            });
        }

        return orderService;
    }

    private static string SpecialtyForWorkshopCategory(string category) => category switch
    {
        "Diagnóstico" => "Diagnóstico",
        "Mantenimiento" => "Mantenimiento",
        "Electricista" or "Eléctrico" => "Electricista",
        "Frenos" => "Frenos",
        _ => "Mantenimiento"
    };

    private static async Task RefreshServiceOrderTotalAsync(AppDbContext context, int serviceOrderId)
    {
        var order = await context.ServiceOrders.AsTracking().FirstAsync(x => x.Id == serviceOrderId);
        order.EstimatedTotal = await context.OrderServices
            .Where(x => x.ServiceOrderId == serviceOrderId)
            .SumAsync(x => x.Price);
    }

    private static async Task EnsureMechanicDiagnosticAsync(AppDbContext context, int serviceOrderId, int mechanicPersonId, int? workshopChiefPersonId, MechanicDiagnosticStatus status, string seedCode, string findings, string recommendedWork, string? chiefComment)
    {
        var diagnostic = await context.MechanicDiagnostics
            .AsTracking()
            .FirstOrDefaultAsync(x => x.ServiceOrderId == serviceOrderId && x.Findings.Contains(seedCode));

        var reviewed = status is MechanicDiagnosticStatus.Approved or MechanicDiagnosticStatus.Rejected;
        if (diagnostic is null)
        {
            diagnostic = new MechanicDiagnostic
            {
                ServiceOrderId = serviceOrderId,
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            };
            await context.MechanicDiagnostics.AddAsync(diagnostic);
        }

        diagnostic.MechanicPersonId = mechanicPersonId;
        diagnostic.WorkshopChiefPersonId = workshopChiefPersonId;
        diagnostic.Status = status;
        diagnostic.Findings = $"{findings} [{seedCode}]";
        diagnostic.RecommendedWork = recommendedWork;
        diagnostic.WorkshopChiefComment = chiefComment;
        diagnostic.SubmittedAt = DateTime.UtcNow.AddDays(-2);
        diagnostic.ReviewedAt = reviewed ? DateTime.UtcNow.AddDays(-1) : null;
    }

    private static async Task EnsureAdditionalServiceRequestAsync(
        AppDbContext context,
        int serviceOrderId,
        int mechanicPersonId,
        int clientPersonId,
        int? workshopChiefPersonId,
        AdditionalRequestStatus status,
        AdditionalRequestType requestType,
        string? workshopServiceName,
        string? partCode,
        int? quantity,
        string seedCode,
        string technicalComment,
        string? workshopChiefComment,
        string? clientComment)
    {
        var request = await context.AdditionalServiceRequests
            .AsTracking()
            .FirstOrDefaultAsync(x => x.ServiceOrderId == serviceOrderId && x.TechnicalComment.Contains(seedCode));

        var workshopService = string.IsNullOrWhiteSpace(workshopServiceName)
            ? null
            : await context.WorkshopServices.AsTracking().FirstOrDefaultAsync(x => x.Name == workshopServiceName);
        var part = string.IsNullOrWhiteSpace(partCode)
            ? null
            : await context.Parts.AsTracking().FirstOrDefaultAsync(x => x.Code == partCode);

        var effectiveQuantity = quantity ?? (part is null ? null : 1);
        var estimatedPrice = 0m;
        if (workshopService is not null)
        {
            estimatedPrice += workshopService.FinalPrice;
        }

        if (part is not null && effectiveQuantity.HasValue)
        {
            estimatedPrice += part.UnitPrice * effectiveQuantity.Value;
        }

        if (estimatedPrice <= 0m)
        {
            estimatedPrice = 50000m;
        }

        var reviewedByChief = status is not AdditionalRequestStatus.PendingWorkshopChiefApproval and not AdditionalRequestStatus.Draft;
        var reviewedByClient = status is AdditionalRequestStatus.ApprovedByClient or AdditionalRequestStatus.RejectedByClient or AdditionalRequestStatus.AddedToOrder;

        if (request is null)
        {
            request = new AdditionalServiceRequest
            {
                ServiceOrderId = serviceOrderId,
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            };
            await context.AdditionalServiceRequests.AddAsync(request);
        }

        request.MechanicPersonId = mechanicPersonId;
        request.ClientPersonId = clientPersonId;
        request.WorkshopChiefPersonId = reviewedByChief ? workshopChiefPersonId : null;
        request.Status = status;
        request.RequestType = requestType;
        request.WorkshopServiceId = workshopService?.Id;
        request.PartId = part?.Id;
        request.Quantity = effectiveQuantity;
        request.TechnicalComment = $"{technicalComment} [{seedCode}]";
        request.WorkshopChiefComment = workshopChiefComment;
        request.ClientComment = clientComment;
        request.EstimatedPrice = estimatedPrice;
        request.WorkshopChiefReviewedAt = reviewedByChief ? DateTime.UtcNow.AddDays(-1) : null;
        request.ClientReviewedAt = reviewedByClient ? DateTime.UtcNow : null;
        request.AddedToOrderAt = status == AdditionalRequestStatus.AddedToOrder ? DateTime.UtcNow : null;
        request.IsActive = true;
    }

    private static async Task<Invoice> EnsureInvoiceAsync(AppDbContext context, int serviceOrderId, string statusName, DateTime invoiceDate)
    {
        var status = await context.InvoiceStatuses.FirstAsync(x => x.Name == statusName);
        var order = await context.ServiceOrders.FirstAsync(x => x.Id == serviceOrderId);
        var invoice = await context.Invoices.AsTracking().FirstOrDefaultAsync(x => x.ServiceOrderId == serviceOrderId);

        if (invoice is null)
        {
            invoice = new Invoice
            {
                ServiceOrderId = serviceOrderId,
                InvoiceStatusId = status.Id,
                InvoiceDate = invoiceDate,
                LaborCost = order.EstimatedTotal,
                Total = order.EstimatedTotal
            };
            await context.Invoices.AddAsync(invoice);
        }

        invoice.InvoiceStatusId = status.Id;
        invoice.InvoiceDate = invoiceDate;
        invoice.LaborCost = order.EstimatedTotal;
        invoice.Total = order.EstimatedTotal;
        await context.SaveChangesAsync();
        return invoice;
    }

    private static async Task EnsureInvoiceDetailAsync(AppDbContext context, int invoiceId, string concept, int quantity, decimal unitPrice)
    {
        var detail = await context.InvoiceDetails.AsTracking().FirstOrDefaultAsync(x => x.InvoiceId == invoiceId && x.Concept == concept);
        if (detail is null)
        {
            detail = new InvoiceDetail { InvoiceId = invoiceId, Concept = concept };
            await context.InvoiceDetails.AddAsync(detail);
        }

        detail.Quantity = quantity;
        detail.UnitPrice = unitPrice;
    }

    private static async Task EnsurePaymentAsync(AppDbContext context, int invoiceId, int clientPersonId, string methodName, string statusName, decimal amount, string reference, DateTime paymentDate, int? verifierPersonId, string? rejectedReason)
    {
        var method = await context.PaymentMethods.FirstAsync(x => x.Name == methodName);
        var status = await context.PaymentStatuses.FirstAsync(x => x.Name == statusName);
        var payment = await context.Payments.AsTracking().FirstOrDefaultAsync(x => x.Reference == reference);

        if (payment is null)
        {
            payment = new Payment { Reference = reference };
            await context.Payments.AddAsync(payment);
        }

        payment.InvoiceId = invoiceId;
        payment.ClientPersonId = clientPersonId;
        payment.PaymentMethodId = method.Id;
        payment.PaymentStatusId = status.Id;
        payment.Amount = amount;
        payment.PaymentDate = paymentDate;
        payment.VerifiedByReceptionistPersonId = verifierPersonId;
        payment.VerifiedAt = verifierPersonId.HasValue ? paymentDate.AddHours(3) : null;
        payment.RejectedReason = rejectedReason;
    }

    private static async Task<PaymentMethod> EnsurePaymentMethodAsync(AppDbContext context, string name)
    {
        var method = await context.PaymentMethods.AsTracking().FirstOrDefaultAsync(x => x.Name == name);
        if (method is not null)
        {
            return method;
        }

        method = new PaymentMethod { Name = name };
        await context.PaymentMethods.AddAsync(method);
        await context.SaveChangesAsync();
        return method;
    }

    private static async Task SeedAuditTrailAsync(AppDbContext context)
    {
        var admin = await GetRequiredUserByEmailAsync(context, "admin@autotaller.com");
        var receptionist = await GetRequiredUserByEmailAsync(context, "recepcionista@autotaller.com");
        var warehouseChief = await GetRequiredUserByEmailAsync(context, "jefebodega@autotaller.com");
        var inventoryManager = await GetRequiredUserByEmailAsync(context, "jefealmacen@autotaller.com");
        var workshopChief = await GetRequiredUserByEmailAsync(context, "jefe.mecanicos@autotaller.com");

        var createActionId = await EnsureAuditActionTypeAsync(context, "CREATE");
        var updateActionId = await EnsureAuditActionTypeAsync(context, "UPDATE");

        await EnsureAuditAsync(context, admin.Id, createActionId, "Users", admin.Id, "Seeder: creación de usuarios base del sistema.", DateTime.UtcNow.AddDays(-5));
        await EnsureAuditAsync(context, receptionist.Id, createActionId, "ServiceOrders", 1, "Seeder: registro de orden de servicio inicial.", DateTime.UtcNow.AddDays(-4));
        await EnsureAuditAsync(context, workshopChief.Id, updateActionId, "MechanicAssignments", 1, "Seeder: asignación de mecánico a servicio.", DateTime.UtcNow.AddDays(-3));
        await EnsureAuditAsync(context, warehouseChief.Id, createActionId, "StockSubmissions", 1, "Seeder: envío de stock desde bodega.", DateTime.UtcNow.AddDays(-2));
        await EnsureAuditAsync(context, inventoryManager.Id, updateActionId, "InventoryHistory", 1, "Seeder: aprobación de inventario inicial.", DateTime.UtcNow.AddDays(-1));
    }

    private static async Task<int> EnsureAuditActionTypeAsync(AppDbContext context, string name)
    {
        var actionType = await context.AuditActionTypes.AsTracking().FirstOrDefaultAsync(x => x.Name == name);
        if (actionType is not null)
        {
            return actionType.Id;
        }

        actionType = new AuditActionType { Name = name };
        await context.AuditActionTypes.AddAsync(actionType);
        await context.SaveChangesAsync();
        return actionType.Id;
    }

    private static async Task EnsureAuditAsync(AppDbContext context, int userId, int actionTypeId, string entity, int recordId, string description, DateTime createdAt)
    {
        var exists = await context.Audits.AnyAsync(x =>
            x.UserId == userId &&
            x.AuditActionTypeId == actionTypeId &&
            x.AffectedEntity == entity &&
            x.AffectedRecordId == recordId &&
            x.Description == description);

        if (exists)
        {
            return;
        }

        await context.Audits.AddAsync(new Audit
        {
            UserId = userId,
            AuditActionTypeId = actionTypeId,
            AffectedEntity = entity,
            AffectedRecordId = recordId,
            Description = description,
            CreatedAt = createdAt
        });

        await context.SaveChangesAsync();
    }

    private static async Task<User> GetRequiredUserByEmailAsync(AppDbContext context, string email)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();
        return await context.Users
            .Include(x => x.Person)
            .ThenInclude(x => x.Emails)
            .ThenInclude(x => x.EmailDomain)
            .FirstAsync(x => x.Person.Emails.Any(personEmail =>
                (personEmail.EmailUser + "@" + personEmail.EmailDomain.Domain).ToLower() == normalizedEmail));
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
        string LastName,
        IReadOnlyList<string>? Specialties = null);

    private sealed record SeedPart(
        string Code,
        string Description,
        int PartCategoryId,
        int? PartBrandId,
        int Stock,
        int MinimumStock,
        decimal UnitPrice);

    private sealed record SeedCustomer(
        string Email,
        string Password,
        string DocumentNumber,
        string FirstName,
        string? MiddleName,
        string LastName,
        string? SecondLastName,
        string PhoneNumber,
        string GenderName,
        DateOnly BirthDate,
        string AddressText);

    private sealed record SeedPersonResult(int PersonId, int UserId);
}
