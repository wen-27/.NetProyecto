using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Admin;

[ApiController]
[Route("api/admin")]
[Authorize(Policy = "AdminOnly")]
// Controlador encargado de exponer por HTTP las operaciones relacionadas con Admin.
public sealed class AdminController : ControllerBase
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard(CancellationToken ct)
    {
        var clientsQuery = _context.Persons
            .AsNoTracking()
            .Where(person => person.PersonRoles.Any(personRole =>
                personRole.IsActive &&
                personRole.Role.RoleName == "Client"));

        var totalClients = await clientsQuery.CountAsync(ct);
        var totalVehicles = await _context.Vehicles.AsNoTracking().CountAsync(ct);
        var totalActiveUsers = await _context.Users.AsNoTracking().CountAsync(user => user.IsActive, ct);
        var totalMechanics = await CountPeopleByRoleAsync("Mechanic", ct);
        var totalReceptionists = await CountPeopleByRoleAsync("Receptionist", ct);
        var totalWorkshopChiefs = await CountPeopleByRoleAsync("WorkshopChief", ct);
        var totalActiveOrders = await _context.ServiceOrders.AsNoTracking().CountAsync(order =>
            order.OrderStatus.Name == "Created" ||
            order.OrderStatus.Name == "PendingAssignment" ||
            order.OrderStatus.Name == "Assigned" ||
            order.OrderStatus.Name == "InProgress" ||
            order.OrderStatus.Name == "PendingClientApproval" ||
            order.OrderStatus.Name == "WaitingForPayment" ||
            order.OrderStatus.Name == "PaymentUnderReview" ||
            order.OrderStatus.Name == "Paid" ||
            order.OrderStatus.Name == "ReadyForDelivery",
            ct);
        var pendingOrders = await _context.ServiceOrders.AsNoTracking().CountAsync(order =>
            order.OrderStatus.Name == "Created" ||
            order.OrderStatus.Name == "PendingAssignment" ||
            order.OrderStatus.Name == "Assigned" ||
            order.OrderStatus.Name == "InProgress" ||
            order.OrderStatus.Name == "PendingClientApproval" ||
            order.OrderStatus.Name == "WaitingForPayment" ||
            order.OrderStatus.Name == "PaymentUnderReview",
            ct);
        var completedOrders = await _context.ServiceOrders.AsNoTracking().CountAsync(order =>
            order.OrderStatus.Name == "Delivered" ||
            order.OrderStatus.Name == "Completed" ||
            order.OrderStatus.Name == "ReadyForDelivery",
            ct);
        var pendingPayments = await _context.Payments.AsNoTracking().CountAsync(payment =>
            payment.PaymentStatus.Name == "PendingPayment" ||
            payment.PaymentStatus.Name == "PendingReceptionVerification" ||
            payment.PaymentStatus.Name == "Pending",
            ct);
        var verifiedPayments = await _context.Payments.AsNoTracking().CountAsync(payment =>
            payment.PaymentStatus.Name == "Approved" ||
            payment.PaymentStatus.Name == "Verified" ||
            payment.PaymentStatus.Name == "Paid",
            ct);
        var pendingInvoices = await _context.Invoices.AsNoTracking().CountAsync(invoice =>
            invoice.InvoiceStatus.Name == "Pending" ||
            invoice.InvoiceStatus.Name == "PendingPayment" ||
            invoice.InvoiceStatus.Name == "PendingReceptionVerification",
            ct);

        var recentOrders = await _context.ServiceOrders
            .AsNoTracking()
            .Include(order => order.OrderStatus)
            .Include(order => order.Vehicle).ThenInclude(vehicle => vehicle.VehicleModel).ThenInclude(model => model.VehicleBrand)
            .Include(order => order.Vehicle).ThenInclude(vehicle => vehicle.OwnerHistory).ThenInclude(owner => owner.Person)
            .OrderByDescending(order => order.EntryDate)
            .Take(6)
            .Select(order => new
            {
                id = order.Id,
                code = $"OT-{order.Id}",
                customer = order.Vehicle.OwnerHistory
                    .Where(owner => owner.EndDate == null)
                    .OrderByDescending(owner => owner.StartDate)
                    .Select(owner => owner.Person.FirstName + " " + (owner.Person.MiddleName ?? "") + " " + owner.Person.LastName + " " + (owner.Person.SecondLastName ?? ""))
                    .FirstOrDefault() ?? "Sin propietario",
                vehicle = order.Vehicle.VehicleModel.VehicleBrand.BrandName + " " + order.Vehicle.VehicleModel.ModelName + " " + order.Vehicle.Plate,
                status = order.OrderStatus.Name,
                entryDate = order.EntryDate,
                estimatedTotal = order.EstimatedTotal
            })
            .ToListAsync(ct);

        var recentPayments = await _context.Payments
            .AsNoTracking()
            .Include(payment => payment.PaymentStatus)
            .Include(payment => payment.PaymentMethod)
            .Include(payment => payment.ClientPerson)
            .Include(payment => payment.Invoice).ThenInclude(invoice => invoice.ServiceOrder).ThenInclude(order => order.Vehicle).ThenInclude(vehicle => vehicle.VehicleModel).ThenInclude(model => model.VehicleBrand)
            .OrderByDescending(payment => payment.PaymentDate)
            .Take(6)
            .Select(payment => new
            {
                id = payment.Id,
                invoiceId = payment.InvoiceId,
                orderCode = $"OT-{payment.Invoice.ServiceOrderId}",
                customer = payment.ClientPerson == null
                    ? "Cliente"
                    : payment.ClientPerson.FirstName + " " + (payment.ClientPerson.MiddleName ?? "") + " " + payment.ClientPerson.LastName + " " + (payment.ClientPerson.SecondLastName ?? ""),
                vehicle = payment.Invoice.ServiceOrder.Vehicle.VehicleModel.VehicleBrand.BrandName + " " + payment.Invoice.ServiceOrder.Vehicle.VehicleModel.ModelName + " " + payment.Invoice.ServiceOrder.Vehicle.Plate,
                amount = payment.Amount,
                method = payment.PaymentMethod.Name,
                status = payment.PaymentStatus.Name,
                date = payment.PaymentDate,
                reference = payment.Reference
            })
            .ToListAsync(ct);

        var recentClients = await clientsQuery
            .Include(person => person.DocumentType)
            .Include(person => person.Emails).ThenInclude(email => email.EmailDomain)
            .OrderByDescending(person => person.CreatedAt)
            .Take(6)
            .Select(person => new
            {
                id = person.Id,
                fullName = person.FirstName + " " + (person.MiddleName ?? "") + " " + person.LastName + " " + (person.SecondLastName ?? ""),
                document = $"{person.DocumentType.Code} {person.DocumentNumber}",
                email = person.Emails
                    .OrderByDescending(email => email.IsPrimary)
                    .Select(email => email.EmailUser + "@" + email.EmailDomain.Domain)
                    .FirstOrDefault(),
                createdAt = person.CreatedAt
            })
            .ToListAsync(ct);

        var recentVehicles = await _context.Vehicles
            .AsNoTracking()
            .Include(vehicle => vehicle.VehicleModel).ThenInclude(model => model.VehicleBrand)
            .Include(vehicle => vehicle.OwnerHistory).ThenInclude(owner => owner.Person)
            .OrderByDescending(vehicle => vehicle.Id)
            .Take(6)
            .Select(vehicle => new
            {
                id = vehicle.Id,
                plate = vehicle.Plate,
                vin = vehicle.Vin,
                vehicle = vehicle.VehicleModel.VehicleBrand.BrandName + " " + vehicle.VehicleModel.ModelName + " " + vehicle.Year,
                owner = vehicle.OwnerHistory
                    .Where(ownerHistory => ownerHistory.EndDate == null)
                    .OrderByDescending(ownerHistory => ownerHistory.StartDate)
                    .Select(ownerHistory => ownerHistory.Person.FirstName + " " + (ownerHistory.Person.MiddleName ?? "") + " " + ownerHistory.Person.LastName + " " + (ownerHistory.Person.SecondLastName ?? ""))
                    .FirstOrDefault() ?? "Sin propietario",
                createdAt = (DateTime?)null
            })
            .ToListAsync(ct);

        var mechanicsBySpecialty = await _context.MechanicSpecialtyAssignments
            .AsNoTracking()
            .GroupBy(assignment => assignment.Specialty.Name)
            .Select(group => new { specialty = group.Key, total = group.Select(assignment => assignment.PersonId).Distinct().Count() })
            .OrderBy(item => item.specialty)
            .ToListAsync(ct);

        var ordersByStatus = await _context.ServiceOrders
            .AsNoTracking()
            .GroupBy(order => order.OrderStatus.Name)
            .Select(group => new { status = group.Key, total = group.Count() })
            .OrderByDescending(item => item.total)
            .ToListAsync(ct);

        return Ok(new
        {
            totals = new
            {
                clients = totalClients,
                vehicles = totalVehicles,
                activeUsers = totalActiveUsers,
                mechanics = totalMechanics,
                receptionists = totalReceptionists,
                workshopChiefs = totalWorkshopChiefs,
                activeOrders = totalActiveOrders,
                pendingOrders,
                completedOrders,
                pendingPayments,
                verifiedPayments,
                pendingInvoices
            },
            recentOrders,
            recentPayments,
            recentClients,
            recentVehicles,
            mechanicsBySpecialty,
            ordersByStatus
        });
    }

    [HttpGet("clients")]
    public async Task<IActionResult> GetClients([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var people = await _context.Persons
            .AsNoTracking()
            .Include(person => person.DocumentType)
            .Include(person => person.Emails).ThenInclude(email => email.EmailDomain)
            .Include(person => person.Phones)
            .Include(person => person.PersonRoles).ThenInclude(personRole => personRole.Role)
            .Include(person => person.VehicleHistory)
            .ToListAsync(ct);

        var filtered = people.Where(person =>
            (person.PersonRoles.Any(personRole => personRole.IsActive && personRole.Role.RoleName == "Client") ||
             person.VehicleHistory.Any()) &&
            !person.PersonRoles.Any(personRole =>
                personRole.IsActive && IsInternalPanelRole(personRole.Role.RoleName)));

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            filtered = filtered.Where(person =>
                person.DocumentNumber.ToLowerInvariant().Contains(term) ||
                person.FirstName.ToLowerInvariant().Contains(term) ||
                person.LastName.ToLowerInvariant().Contains(term) ||
                person.Emails.Any(email => $"{email.EmailUser}@{email.EmailDomain.Domain}".ToLowerInvariant().Contains(term)));
        }

        var clientEntities = filtered
            .OrderByDescending(person => person.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var clients = clientEntities.Select(person => new
        {
            id = person.Id,
            documentType = person.DocumentType.Code,
            documentNumber = person.DocumentNumber,
            fullName = string.Join(' ', new[] { person.FirstName, person.MiddleName, person.LastName, person.SecondLastName }.Where(value => !string.IsNullOrWhiteSpace(value))),
            roles = person.PersonRoles
                .Where(personRole => personRole.IsActive)
                .Select(personRole => personRole.Role.RoleName)
                .DefaultIfEmpty("Client")
                .ToArray(),
            primaryEmail = person.Emails
                .OrderByDescending(email => email.IsPrimary)
                .Select(email => email.EmailUser + "@" + email.EmailDomain.Domain)
                .FirstOrDefault() ?? "Sin correo",
            primaryPhone = person.Phones
                .OrderByDescending(phone => phone.IsPrimary)
                .Select(phone => phone.PhoneNumber)
                .FirstOrDefault() ?? "Sin telefono",
            vehiclesCount = person.VehicleHistory.Count(history => history.EndDate == null),
            status = person.IsActive ? "Activo" : "Inactivo",
            role = "Client"
        });

        Response.Headers["X-Total-Count"] = filtered.Count().ToString();
        return Ok(clients);
    }

    [HttpGet("clients/{id:int}")]
    public async Task<IActionResult> GetClient(int id, CancellationToken ct)
    {
        var person = await _context.Persons
            .AsNoTracking()
            .Include(client => client.DocumentType)
            .Include(client => client.Gender)
            .Include(client => client.Address!).ThenInclude(address => address.Neighborhood).ThenInclude(neighborhood => neighborhood.City)
            .Include(client => client.Emails).ThenInclude(email => email.EmailDomain)
            .Include(client => client.Phones)
            .Include(client => client.PersonRoles).ThenInclude(personRole => personRole.Role)
            .Include(client => client.VehicleHistory)
            .FirstOrDefaultAsync(client => client.Id == id, ct);

        if (person is null || !IsAdminClient(person))
        {
            return NotFound(new { message = "El cliente no existe." });
        }

        return Ok(ToAdminClientDto(person));
    }

    [HttpGet("clients/{id:int}/vehicles")]
    public async Task<IActionResult> GetClientVehicles(int id, CancellationToken ct)
    {
        var person = await _context.Persons
            .AsNoTracking()
            .Include(client => client.PersonRoles).ThenInclude(personRole => personRole.Role)
            .Include(client => client.VehicleHistory)
            .FirstOrDefaultAsync(client => client.Id == id, ct);

        if (person is null || !IsAdminClient(person))
        {
            return NotFound(new { message = "El cliente no existe." });
        }

        var vehicles = await _context.Vehicles
            .AsNoTracking()
            .Include(vehicle => vehicle.VehicleModel).ThenInclude(model => model.VehicleBrand)
            .Include(vehicle => vehicle.VehicleType)
            .Include(vehicle => vehicle.OwnerHistory)
            .Where(vehicle => vehicle.OwnerHistory.Any(owner => owner.PersonId == id && owner.EndDate == null))
            .OrderBy(vehicle => vehicle.VehicleModel.VehicleBrand.BrandName)
            .Select(vehicle => new
            {
                id = vehicle.Id,
                plate = vehicle.Plate,
                vin = vehicle.Vin,
                brand = vehicle.VehicleModel.VehicleBrand.BrandName,
                model = vehicle.VehicleModel.ModelName,
                type = vehicle.VehicleType.Name,
                year = vehicle.Year,
                color = vehicle.Color,
                mileage = vehicle.Mileage,
                isActive = vehicle.IsActive
            })
            .ToListAsync(ct);

        return Ok(vehicles);
    }

    private static bool IsInternalPanelRole(string roleName)
    {
        return roleName == "Admin" ||
            roleName == "Mechanic" ||
            roleName == "Receptionist" ||
            roleName == "WorkshopChief" ||
            roleName == "WarehouseChief" ||
            roleName == "InventoryManager";
    }

    private static bool IsAdminClient(Domain.Entities.Person person)
    {
        return (person.PersonRoles.Any(personRole => personRole.IsActive && personRole.Role.RoleName == "Client") ||
                person.VehicleHistory.Any()) &&
            !person.PersonRoles.Any(personRole => personRole.IsActive && IsInternalPanelRole(personRole.Role.RoleName));
    }

    private static object ToAdminClientDto(Domain.Entities.Person person)
    {
        return new
        {
            id = person.Id,
            documentType = person.DocumentType.Code,
            documentNumber = person.DocumentNumber,
            fullName = string.Join(' ', new[] { person.FirstName, person.MiddleName, person.LastName, person.SecondLastName }.Where(value => !string.IsNullOrWhiteSpace(value))),
            roles = person.PersonRoles
                .Where(personRole => personRole.IsActive)
                .Select(personRole => personRole.Role.RoleName)
                .DefaultIfEmpty("Client")
                .ToArray(),
            primaryEmail = person.Emails
                .OrderByDescending(email => email.IsPrimary)
                .Select(email => email.EmailUser + "@" + email.EmailDomain.Domain)
                .FirstOrDefault() ?? "Sin correo",
            primaryPhone = person.Phones
                .OrderByDescending(phone => phone.IsPrimary)
                .Select(phone => phone.PhoneNumber)
                .FirstOrDefault() ?? "Sin telefono",
            vehiclesCount = person.VehicleHistory.Count(history => history.EndDate == null),
            status = person.IsActive ? "Activo" : "Inactivo",
            role = "Client",
            gender = person.Gender?.Name,
            birthDate = person.BirthDate,
            address = person.Address is null
                ? null
                : string.Join(' ', new[] { person.Address.Complement, person.Address.Neighborhood?.City?.Name }.Where(value => !string.IsNullOrWhiteSpace(value)))
        };
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var userEntities = await _context.Users
            .AsNoTracking()
            .Include(user => user.Person).ThenInclude(person => person.Emails).ThenInclude(email => email.EmailDomain)
            .Include(user => user.Person).ThenInclude(person => person.PersonRoles).ThenInclude(personRole => personRole.Role)
            .Include(user => user.Person).ThenInclude(person => person.Phones)
            .Include(user => user.Person).ThenInclude(person => person.DocumentType)
            .ToListAsync(ct);

        var filteredUsers = userEntities
            .Select(user =>
            {
                var email = user.Person.Emails
                    .OrderByDescending(personEmail => personEmail.IsPrimary)
                    .Select(personEmail => $"{personEmail.EmailUser}@{personEmail.EmailDomain.Domain}")
                    .FirstOrDefault() ?? "";
                var roles = user.Person.PersonRoles
                    .Where(personRole => personRole.IsActive)
                    .Select(personRole => personRole.Role.RoleName)
                    .Distinct()
                    .ToList();

                var seedRole = GetSeedRoleForEmail(email);
                if (roles.Count == 0 && seedRole is not null)
                {
                    roles.Add(seedRole);
                }

                if (roles.Count == 0)
                {
                    roles.Add("Client");
                }

                return new
                {
                    User = user,
                    Email = email,
                    Roles = roles
                };
            });

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            filteredUsers = filteredUsers.Where(user =>
                user.User.Person.DocumentNumber.ToLowerInvariant().Contains(term) ||
                user.User.Person.FirstName.ToLowerInvariant().Contains(term) ||
                user.User.Person.LastName.ToLowerInvariant().Contains(term) ||
                user.Email.ToLowerInvariant().Contains(term) ||
                user.Roles.Any(role => role.ToLowerInvariant().Contains(term)));
        }

        var total = filteredUsers.Count();
        var users = filteredUsers
            .OrderByDescending(user => user.User.CreatedAt)
            .Skip((Math.Max(1, pageNumber) - 1) * Math.Max(1, pageSize))
            .Take(Math.Max(1, pageSize))
            .Select(user => new
            {
                id = user.User.Id,
                personId = user.User.PersonId,
                name = string.Join(' ', new[] { user.User.Person.FirstName, user.User.Person.MiddleName, user.User.Person.LastName, user.User.Person.SecondLastName }.Where(value => !string.IsNullOrWhiteSpace(value))),
                document = user.User.Person.DocumentType.Code + " " + user.User.Person.DocumentNumber,
                email = user.Email,
                phone = user.User.Person.Phones
                    .OrderByDescending(personPhone => personPhone.IsPrimary)
                    .Select(personPhone => personPhone.PhoneNumber)
                    .FirstOrDefault() ?? "",
                roles = user.Roles.ToArray(),
                status = user.User.IsActive ? "Activo" : "Inactivo",
                createdAt = user.User.CreatedAt,
                lastAccess = user.User.CreatedAt
            })
            .ToList();

        Response.Headers["X-Total-Count"] = total.ToString();
        return Ok(users);
    }

    private static string? GetSeedRoleForEmail(string email)
    {
        return email.ToLowerInvariant() switch
        {
            "admin@autotaller.com" => "Admin",
            "recepcionista@autotaller.com" => "Receptionist",
            "jefe.mecanicos@autotaller.com" => "WorkshopChief",
            "jefebodega@autotaller.com" => "WarehouseChief",
            "jefealmacen@autotaller.com" => "InventoryManager",
            "mecanico@autotaller.com" => "Mechanic",
            "diagnostico@autotaller.com" => "Mechanic",
            "mantenimiento@autotaller.com" => "Mechanic",
            "electricista@autotaller.com" => "Mechanic",
            "frenos@autotaller.com" => "Mechanic",
            "carlos.ramirez@test.com" => "Client",
            "laura.gomez@test.com" => "Client",
            "client@mail.com" => "Client",
            "admin@mail.com" => "Admin",
            "mechanic@mail.com" => "Mechanic",
            "receptionist@mail.com" => "Receptionist",
            _ => null
        };
    }

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser(CreateAdminUserRequest request, CancellationToken ct)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var emailParts = SplitEmail(normalizedEmail);
        if (emailParts is null)
        {
            return BadRequest(new { message = "El correo no tiene un formato válido." });
        }

        if (string.IsNullOrWhiteSpace(request.FirstName) ||
            string.IsNullOrWhiteSpace(request.LastName) ||
            string.IsNullOrWhiteSpace(request.DocumentNumber))
        {
            return BadRequest(new { message = "Nombre, apellido y documento son obligatorios." });
        }

        if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 8)
        {
            return BadRequest(new { message = "La contraseña debe tener al menos 8 caracteres." });
        }

        var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == request.RoleId, ct);
        if (role is null)
        {
            return BadRequest(new { message = "El rol seleccionado no existe." });
        }

        if (role.RoleName == "Mechanic" && !request.MechanicSpecialtyId.HasValue)
        {
            return BadRequest(new { message = "El mecánico debe tener una especialidad asignada." });
        }

        if (request.MechanicSpecialtyId.HasValue && !await _context.MechanicSpecialties.AnyAsync(x => x.Id == request.MechanicSpecialtyId.Value, ct))
        {
            return BadRequest(new { message = "La especialidad seleccionada no existe." });
        }

        if (!await _context.DocumentTypes.AnyAsync(x => x.Id == request.DocumentTypeId, ct))
        {
            return BadRequest(new { message = "El tipo de documento no existe." });
        }

        var documentExists = await _context.Persons.AnyAsync(x =>
            x.DocumentTypeId == request.DocumentTypeId &&
            x.DocumentNumber == request.DocumentNumber.Trim(),
            ct);
        if (documentExists)
        {
            return Conflict(new { message = "Ya existe una persona con ese documento." });
        }

        var emailExists = await _context.PersonEmails
            .Include(x => x.EmailDomain)
            .AnyAsync(x => (x.EmailUser + "@" + x.EmailDomain.Domain).ToLower() == normalizedEmail, ct);
        if (emailExists)
        {
            return Conflict(new { message = "Ya existe un usuario registrado con ese correo." });
        }

        await using var transaction = await _context.Database.BeginTransactionAsync(ct);

        var person = new Domain.Entities.Person
        {
            DocumentTypeId = request.DocumentTypeId,
            DocumentNumber = request.DocumentNumber.Trim(),
            FirstName = request.FirstName.Trim(),
            MiddleName = string.IsNullOrWhiteSpace(request.MiddleName) ? null : request.MiddleName.Trim(),
            LastName = request.LastName.Trim(),
            SecondLastName = string.IsNullOrWhiteSpace(request.SecondLastName) ? null : request.SecondLastName.Trim(),
            IsActive = request.IsActive
        };

        await _context.Persons.AddAsync(person, ct);
        await _context.SaveChangesAsync(ct);

        var domain = await _context.EmailDomains.FirstOrDefaultAsync(x => x.Domain == emailParts.Value.Domain, ct);
        if (domain is null)
        {
            domain = new Domain.Entities.EmailDomain { Domain = emailParts.Value.Domain };
            await _context.EmailDomains.AddAsync(domain, ct);
            await _context.SaveChangesAsync(ct);
        }

        await _context.PersonEmails.AddAsync(new Domain.Entities.PersonEmail
        {
            PersonId = person.Id,
            EmailDomainId = domain.Id,
            EmailUser = emailParts.Value.User,
            IsPrimary = true
        }, ct);

        if (!string.IsNullOrWhiteSpace(request.Phone))
        {
            await _context.PersonPhones.AddAsync(new Domain.Entities.PersonPhone
            {
                PersonId = person.Id,
                CountryId = request.PhoneCountryId ?? 1,
                PhoneNumber = request.Phone.Trim(),
                IsPrimary = true
            }, ct);
        }

        await _context.PersonRoles.AddAsync(new Domain.Entities.PersonRole
        {
            PersonId = person.Id,
            RoleId = role.Id,
            IsActive = true
        }, ct);

        if (request.MechanicSpecialtyId.HasValue)
        {
            await _context.MechanicSpecialtyAssignments.AddAsync(new Domain.Entities.MechanicSpecialtyAssignment
            {
                PersonId = person.Id,
                SpecialtyId = request.MechanicSpecialtyId.Value
            }, ct);
        }

        var user = new Domain.Entities.User
        {
            PersonId = person.Id,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            IsActive = request.IsActive
        };

        await _context.Users.AddAsync(user, ct);
        await _context.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);

        return Created($"/api/admin/users/{user.Id}", new { id = user.Id, personId = person.Id });
    }

    [HttpPatch("users/{id:int}/status")]
    public async Task<IActionResult> ChangeUserStatus(int id, ChangeAdminUserStatusRequest request, CancellationToken ct)
    {
        var user = await _context.Users.AsTracking().Include(x => x.Person).FirstOrDefaultAsync(x => x.Id == id, ct);
        if (user is null)
        {
            return NotFound(new { message = "El usuario no existe." });
        }

        user.IsActive = request.IsActive;
        user.Person.IsActive = request.IsActive;
        await _context.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpPut("users/{id:int}/roles")]
    public async Task<IActionResult> UpdateUserRoles(int id, UpdateAdminUserRolesRequest request, CancellationToken ct)
    {
        var user = await _context.Users.AsTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        if (user is null)
        {
            return NotFound(new { message = "El usuario no existe." });
        }

        var roleNames = (request.RoleNames ?? Array.Empty<string>())
            .Select(NormalizePanelRoleName)
            .Where(roleName => roleName is not null)
            .Select(roleName => roleName!)
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        if (roleNames.Length == 0)
        {
            return BadRequest(new { message = "El usuario debe tener al menos un rol." });
        }

        var selectedRoles = await _context.Roles
            .AsTracking()
            .Where(role => roleNames.Contains(role.RoleName))
            .ToListAsync(ct);

        var missingRoleNames = roleNames.Except(selectedRoles.Select(role => role.RoleName)).ToArray();
        foreach (var roleName in missingRoleNames)
        {
            var role = new Domain.Entities.Role { RoleName = roleName, IsActive = true };
            await _context.Roles.AddAsync(role, ct);
            selectedRoles.Add(role);
        }

        if (missingRoleNames.Length > 0)
        {
            await _context.SaveChangesAsync(ct);
        }

        var personRoles = await _context.PersonRoles
            .AsTracking()
            .Include(personRole => personRole.Role)
            .Where(personRole => personRole.PersonId == user.PersonId)
            .ToListAsync(ct);

        foreach (var personRole in personRoles.Where(personRole => NormalizePanelRoleName(personRole.Role.RoleName) is not null))
        {
            personRole.IsActive = roleNames.Contains(personRole.Role.RoleName);
        }

        foreach (var role in selectedRoles)
        {
            var personRole = personRoles.FirstOrDefault(x => x.RoleId == role.Id);
            if (personRole is null)
            {
                await _context.PersonRoles.AddAsync(new Domain.Entities.PersonRole
                {
                    PersonId = user.PersonId,
                    RoleId = role.Id,
                    IsActive = true
                }, ct);
            }
            else
            {
                personRole.IsActive = true;
            }
        }

        await _context.SaveChangesAsync(ct);
        return NoContent();
    }

    private static string? NormalizePanelRoleName(string? roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            return null;
        }

        var normalized = roleName.Trim();
        return normalized switch
        {
            "Admin" => "Admin",
            "Receptionist" => "Receptionist",
            "Mechanic" => "Mechanic",
            "Client" => "Client",
            "WorkshopChief" => "WorkshopChief",
            "WarehouseChief" => "WarehouseChief",
            "InventoryManager" => "InventoryManager",
            _ => null
        };
    }

    private Task<int> CountPeopleByRoleAsync(string roleName, CancellationToken ct)
    {
        return _context.PersonRoles
            .AsNoTracking()
            .Where(personRole => personRole.Role.RoleName == roleName)
            .Select(personRole => personRole.PersonId)
            .Distinct()
            .CountAsync(ct);
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
}

// Controlador encargado de exponer por HTTP las operaciones relacionadas con CreateAdminUserRequest.
public sealed record CreateAdminUserRequest(
    int DocumentTypeId,
    string DocumentNumber,
    string FirstName,
    string? MiddleName,
    string LastName,
    string? SecondLastName,
    string Email,
    string? Phone,
    int? PhoneCountryId,
    string Password,
    int RoleId,
    int? MechanicSpecialtyId,
    bool IsActive);

// Controlador encargado de exponer por HTTP las operaciones relacionadas con ChangeAdminUserStatusRequest.
public sealed record ChangeAdminUserStatusRequest(bool IsActive);
// Controlador encargado de exponer por HTTP las operaciones relacionadas con UpdateAdminUserRolesRequest.
public sealed record UpdateAdminUserRolesRequest(string[]? RoleNames);
