using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Admin;

[ApiController]
[Route("api/admin")]
[Authorize(Policy = "AdminOnly")]
public sealed class AdminController : ControllerBase
{
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
            .Where(person => !person.PersonRoles.Any(personRole =>
                personRole.Role.RoleName == "Admin" ||
                personRole.Role.RoleName == "Mechanic" ||
                personRole.Role.RoleName == "Receptionist" ||
                personRole.Role.RoleName == "WorkshopChief" ||
                personRole.Role.RoleName == "WarehouseChief" ||
                personRole.Role.RoleName == "InventoryManager"));

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
                vehicle = order.Vehicle.VehicleModel.VehicleBrand.BrandName + " " + order.Vehicle.VehicleModel.ModelName + " " + order.Vehicle.Vin,
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
                vehicle = payment.Invoice.ServiceOrder.Vehicle.VehicleModel.VehicleBrand.BrandName + " " + payment.Invoice.ServiceOrder.Vehicle.VehicleModel.ModelName + " " + payment.Invoice.ServiceOrder.Vehicle.Vin,
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

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var query = _context.Users
            .AsNoTracking()
            .Include(user => user.Person).ThenInclude(person => person.Emails).ThenInclude(email => email.EmailDomain)
            .Include(user => user.Person).ThenInclude(person => person.PersonRoles).ThenInclude(personRole => personRole.Role)
            .Include(user => user.Person).ThenInclude(person => person.Phones)
            .Include(user => user.Person).ThenInclude(person => person.DocumentType)
            .Include(user => user.Person).ThenInclude(person => person.User)
            .Include(user => user.Person)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(user =>
                user.Person.DocumentNumber.ToLower().Contains(term) ||
                user.Person.FirstName.ToLower().Contains(term) ||
                user.Person.LastName.ToLower().Contains(term) ||
                user.Person.Emails.Any(email => (email.EmailUser + "@" + email.EmailDomain.Domain).ToLower().Contains(term)) ||
                user.Person.PersonRoles.Any(personRole => personRole.Role.RoleName.ToLower().Contains(term)));
        }

        var total = await query.CountAsync(ct);
        var users = await query
            .OrderByDescending(user => user.CreatedAt)
            .Skip((Math.Max(1, pageNumber) - 1) * Math.Max(1, pageSize))
            .Take(Math.Max(1, pageSize))
            .Select(user => new
            {
                id = user.Id,
                personId = user.PersonId,
                name = user.Person.FirstName + " " + (user.Person.MiddleName ?? "") + " " + user.Person.LastName + " " + (user.Person.SecondLastName ?? ""),
                document = user.Person.DocumentType.Code + " " + user.Person.DocumentNumber,
                email = user.Person.Emails
                    .OrderByDescending(email => email.IsPrimary)
                    .Select(email => email.EmailUser + "@" + email.EmailDomain.Domain)
                    .FirstOrDefault() ?? "",
                phone = user.Person.Phones
                    .OrderByDescending(phone => phone.IsPrimary)
                    .Select(phone => phone.PhoneNumber)
                    .FirstOrDefault() ?? "",
                roles = user.Person.PersonRoles
                    .Where(personRole => personRole.IsActive)
                    .Select(personRole => personRole.Role.RoleName)
                    .ToArray(),
                status = user.IsActive ? "Activo" : "Inactivo",
                createdAt = user.CreatedAt,
                lastAccess = user.CreatedAt
            })
            .ToListAsync(ct);

        Response.Headers["X-Total-Count"] = total.ToString();
        return Ok(users);
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

public sealed record ChangeAdminUserStatusRequest(bool IsActive);
