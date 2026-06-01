using Application.Abstractions.OperationalWorkflow;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Operational;

[Route("api/reception")]
[Authorize(Roles = "Receptionist,Admin")]
public sealed class ReceptionController : OperationalControllerBase
{
    private readonly IOperationalWorkflowService _workflow;
    private readonly AppDbContext _context;

    public ReceptionController(IOperationalWorkflowService workflow, AppDbContext context)
    {
        _workflow = workflow;
        _context = context;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard(CancellationToken ct)
    {
        var clients = await CustomerQuery(null).CountAsync(ct);
        var vehicles = await _context.Vehicles.CountAsync(ct);
        var pendingPayments = await _context.Payments.CountAsync(x => x.PaymentStatus.Name == "PendingReceptionVerification", ct);
        var approvedPayments = await _context.Payments.CountAsync(x => x.PaymentStatus.Name == "Approved", ct);
        var recentOrders = await _context.ServiceOrders.CountAsync(x => x.CreatedAt >= DateTime.UtcNow.AddDays(-7), ct);
        return Ok(new { clients, vehicles, pendingPayments, approvedPayments, recentOrders });
    }

    [HttpGet("customers")]
    public async Task<IActionResult> GetCustomers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var query = CustomerQuery(search);
        var total = await query.CountAsync(ct);
        var customerEntities = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        Response.Headers["X-Total-Count"] = total.ToString();
        return Ok(customerEntities.Select(ToCustomerDto));
    }

    [HttpGet("customers/{id:int}")]
    public async Task<IActionResult> GetCustomer(int id, CancellationToken ct)
    {
        var customer = await CustomerQuery(null).FirstOrDefaultAsync(x => x.Id == id, ct);
        return customer is null ? NotFound(new { message = "El cliente no existe." }) : Ok(ToCustomerDto(customer));
    }

    [HttpPost("customers")]
    public async Task<IActionResult> CreateCustomer(CreateReceptionCustomerRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
            return BadRequest(new { message = "Nombre y apellido son obligatorios." });
        if (string.IsNullOrWhiteSpace(request.DocumentNumber))
            return BadRequest(new { message = "El documento es obligatorio." });
        if (!string.IsNullOrWhiteSpace(request.Email) && !request.Email.Contains('@'))
            return BadRequest(new { message = "El correo no tiene un formato válido." });

        var existsDocument = await _context.Persons.AnyAsync(x => x.DocumentTypeId == request.DocumentTypeId && x.DocumentNumber == request.DocumentNumber.Trim(), ct);
        if (existsDocument) return Conflict(new { message = "Ya existe un cliente con ese documento." });

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            var emailParts = SplitEmail(request.Email);
            if (emailParts is null) return BadRequest(new { message = "El correo no tiene un formato válido." });
            var existsEmail = await _context.PersonEmails.AnyAsync(x => x.EmailUser == emailParts.Value.User && x.EmailDomain.Domain == emailParts.Value.Domain, ct);
            if (existsEmail) return Conflict(new { message = "Ya existe un cliente con ese correo." });
        }

        if (!await _context.DocumentTypes.AnyAsync(x => x.Id == request.DocumentTypeId, ct))
            return BadRequest(new { message = "El tipo de documento no existe." });

        var person = new Person
        {
            DocumentTypeId = request.DocumentTypeId,
            DocumentNumber = request.DocumentNumber.Trim(),
            FirstName = request.FirstName.Trim(),
            MiddleName = request.MiddleName?.Trim(),
            LastName = request.LastName.Trim(),
            SecondLastName = request.SecondLastName?.Trim(),
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        var clientRole = await _context.Roles.FirstAsync(x => x.RoleName == "Client", ct);
        person.PersonRoles.Add(new PersonRole { RoleId = clientRole.Id });

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            var emailParts = SplitEmail(request.Email)!.Value;
            var domain = await _context.EmailDomains.FirstOrDefaultAsync(x => x.Domain == emailParts.Domain, ct);
            if (domain is null)
            {
                domain = new EmailDomain { Domain = emailParts.Domain };
                await _context.EmailDomains.AddAsync(domain, ct);
            }
            person.Emails.Add(new PersonEmail { EmailDomain = domain, EmailUser = emailParts.User, IsPrimary = true });
        }

        if (!string.IsNullOrWhiteSpace(request.Phone))
        {
            person.Phones.Add(new PersonPhone { CountryId = request.PhoneCountryId ?? 1, PhoneNumber = request.Phone.Trim(), IsPrimary = true });
        }

        await _context.Persons.AddAsync(person, ct);
        await _context.SaveChangesAsync(ct);
        return Created($"/api/reception/customers/{person.Id}", new { person.Id });
    }

    [HttpGet("customers/{id:int}/vehicles")]
    public async Task<IActionResult> GetCustomerVehicles(int id, CancellationToken ct)
    {
        var vehicleEntities = await VehicleQuery(null)
            .Where(x => x.OwnerHistory.Any(owner => owner.PersonId == id && owner.EndDate == null))
            .OrderBy(x => x.VehicleModel.VehicleBrand.BrandName)
            .ToListAsync(ct);
        return Ok(vehicleEntities.Select(ToVehicleDto));
    }

    [HttpGet("vehicles")]
    public async Task<IActionResult> GetVehicles([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, [FromQuery] int? clientPersonId = null, CancellationToken ct = default)
    {
        var query = VehicleQuery(search);
        if (clientPersonId.HasValue)
        {
            query = query.Where(x => x.OwnerHistory.Any(owner => owner.PersonId == clientPersonId && owner.EndDate == null));
        }

        var total = await query.CountAsync(ct);
        var vehicleEntities = await query
            .OrderByDescending(x => x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
        Response.Headers["X-Total-Count"] = total.ToString();
        return Ok(vehicleEntities.Select(ToVehicleDto));
    }

    [HttpGet("vehicles/{id:int}")]
    public async Task<IActionResult> GetVehicle(int id, CancellationToken ct)
    {
        var vehicle = await VehicleQuery(null).FirstOrDefaultAsync(x => x.Id == id, ct);
        return vehicle is null ? NotFound(new { message = "El vehículo no existe." }) : Ok(ToVehicleDto(vehicle));
    }

    [HttpPost("vehicles")]
    public async Task<IActionResult> CreateVehicle(CreateReceptionVehicleRequest request, CancellationToken ct)
    {
        if (request.OwnerPersonId <= 0) return BadRequest(new { message = "El propietario es obligatorio." });
        if (string.IsNullOrWhiteSpace(request.Vin)) return BadRequest(new { message = "La placa/VIN es obligatoria." });
        if (await _context.Vehicles.AnyAsync(x => x.Vin == request.Vin.Trim(), ct)) return Conflict(new { message = "Ya existe un vehículo con esa placa/VIN." });
        if (!await _context.Persons.AnyAsync(x => x.Id == request.OwnerPersonId, ct)) return BadRequest(new { message = "El cliente propietario no existe." });
        if (!await _context.VehicleModels.AnyAsync(x => x.Id == request.ModelId, ct)) return BadRequest(new { message = "El modelo no existe." });
        if (!await _context.VehicleTypes.AnyAsync(x => x.Id == request.VehicleTypeId, ct)) return BadRequest(new { message = "El tipo de vehículo no existe." });

        var vehicle = new Vehicle
        {
            ModelId = request.ModelId,
            VehicleTypeId = request.VehicleTypeId,
            Vin = request.Vin.Trim(),
            Year = request.Year,
            Color = request.Color?.Trim(),
            Mileage = request.Mileage,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        vehicle.OwnerHistory.Add(new Domain.Entities.VehicleOwnerHistory { PersonId = request.OwnerPersonId, StartDate = request.StartDate ?? DateTime.UtcNow });
        await _context.Vehicles.AddAsync(vehicle, ct);
        await _context.SaveChangesAsync(ct);
        return Created($"/api/reception/vehicles/{vehicle.Id}", new { vehicle.Id });
    }

    [HttpGet("vehicles/{id:int}/owner-history")]
    public async Task<IActionResult> GetVehicleOwnerHistory(int id, CancellationToken ct)
    {
        var history = await _context.VehicleOwnerHistory
            .Include(x => x.Person)
            .Where(x => x.VehicleId == id)
            .OrderByDescending(x => x.StartDate)
            .ToListAsync(ct);
        return Ok(history.Select(ToOwnerHistoryDto));
    }

    [HttpPost("vehicles/{id:int}/transfer-owner")]
    public async Task<IActionResult> TransferVehicleOwner(int id, TransferVehicleOwnerRequest request, CancellationToken ct)
    {
        if (!await _context.Vehicles.AnyAsync(x => x.Id == id, ct)) return NotFound(new { message = "El vehículo no existe." });
        if (!await _context.Persons.AnyAsync(x => x.Id == request.NewOwnerPersonId, ct)) return BadRequest(new { message = "El nuevo propietario no existe." });

        var transferDate = request.TransferDate ?? DateTime.UtcNow;
        var currentOwner = await _context.VehicleOwnerHistory.AsTracking().FirstOrDefaultAsync(x => x.VehicleId == id && x.EndDate == null, ct);
        if (currentOwner?.PersonId == request.NewOwnerPersonId) return BadRequest(new { message = "El cliente seleccionado ya es el propietario actual." });
        if (currentOwner is not null) currentOwner.EndDate = transferDate;

        await _context.VehicleOwnerHistory.AddAsync(new Domain.Entities.VehicleOwnerHistory
        {
            VehicleId = id,
            PersonId = request.NewOwnerPersonId,
            StartDate = transferDate
        }, ct);
        await _context.SaveChangesAsync(ct);
        return Ok(new { message = "Propietario transferido correctamente." });
    }

    [HttpGet("payments")]
    public async Task<IActionResult> GetPayments([FromQuery] string? search = null, [FromQuery] string? status = null, CancellationToken ct = default)
    {
        var query = PaymentQuery();
        if (!string.IsNullOrWhiteSpace(status)) query = query.Where(x => x.PaymentStatus.Name == status);
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(x =>
                (x.ClientPerson != null && (x.ClientPerson.DocumentNumber.ToLower().Contains(term) || x.ClientPerson.FirstName.ToLower().Contains(term) || x.ClientPerson.LastName.ToLower().Contains(term))) ||
                (x.Reference != null && x.Reference.ToLower().Contains(term)) ||
                x.InvoiceId.ToString().Contains(term) ||
                x.Invoice.ServiceOrderId.ToString().Contains(term));
        }
        var payments = await query.OrderByDescending(x => x.PaymentDate).ToListAsync(ct);
        return Ok(payments.Select(ToReceptionPaymentDto));
    }

    [HttpGet("invoices")]
    public Task<IActionResult> GetInvoices(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetReceptionInvoicesAsync(ct)));

    [HttpGet("payments/pending-verification")]
    public Task<IActionResult> GetPendingPayments(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetPendingPaymentsAsync(ct)));

    [HttpGet("payments/{paymentId:int}")]
    public Task<IActionResult> GetPayment(int paymentId, CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetPaymentAsync(paymentId, ct)));

    [HttpPost("payments/{paymentId:int}/approve")]
    public Task<IActionResult> ApprovePayment(int paymentId, ReviewPaymentDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.ApprovePaymentAsync(CurrentPersonId(), paymentId, dto, ct)));

    [HttpPost("payments/{paymentId:int}/reject")]
    public Task<IActionResult> RejectPayment(int paymentId, ReviewPaymentDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.RejectPaymentAsync(CurrentPersonId(), paymentId, dto, ct)));

    [HttpPost("orders/{orderId:int}/confirm-delivery-date")]
    public Task<IActionResult> ConfirmDeliveryDate(int orderId, ConfirmDeliveryDateDto dto, CancellationToken ct) =>
        ExecuteAsync(async () =>
        {
            await _workflow.ConfirmDeliveryDateAsync(CurrentPersonId(), orderId, dto, ct);
            return NoContent();
        });

    private IQueryable<Person> CustomerQuery(string? search) {
        var query = _context.Persons
            .AsNoTracking()
            .Include(x => x.DocumentType)
            .Include(x => x.Emails).ThenInclude(x => x.EmailDomain)
            .Include(x => x.Phones)
            .Include(x => x.PersonRoles).ThenInclude(x => x.Role)
            .Include(x => x.VehicleHistory)
            .Where(x => !x.PersonRoles.Any(role =>
                role.RoleId == 1 ||
                role.RoleId == 3 ||
                role.RoleId == 4 ||
                role.RoleId == 5 ||
                role.RoleId == 6 ||
                role.RoleId == 7));
        if (string.IsNullOrWhiteSpace(search)) return query;
        var term = search.Trim().ToLower();
        return query.Where(x => x.DocumentNumber.ToLower().Contains(term) || x.FirstName.ToLower().Contains(term) || x.LastName.ToLower().Contains(term) || x.Emails.Any(email => (email.EmailUser + "@" + email.EmailDomain.Domain).ToLower().Contains(term)));
    }

    private IQueryable<Vehicle> VehicleQuery(string? search) {
        var query = _context.Vehicles
            .AsNoTracking()
            .Include(x => x.VehicleModel).ThenInclude(x => x.VehicleBrand)
            .Include(x => x.VehicleType)
            .Include(x => x.OwnerHistory).ThenInclude(x => x.Person)
            .Include(x => x.ServiceOrders);
        if (string.IsNullOrWhiteSpace(search)) return query;
        var term = search.Trim().ToLower();
        return query.Where(x => x.Vin.ToLower().Contains(term) || x.VehicleModel.ModelName.ToLower().Contains(term) || x.VehicleModel.VehicleBrand.BrandName.ToLower().Contains(term) || x.OwnerHistory.Any(owner => owner.EndDate == null && (owner.Person.FirstName.ToLower().Contains(term) || owner.Person.LastName.ToLower().Contains(term) || owner.Person.DocumentNumber.ToLower().Contains(term))));
    }

    private IQueryable<Payment> PaymentQuery() => _context.Payments
        .AsNoTracking()
        .Include(x => x.PaymentStatus)
        .Include(x => x.PaymentMethod)
        .Include(x => x.ClientPerson)
        .Include(x => x.Invoice).ThenInclude(x => x.ServiceOrder).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleModel).ThenInclude(x => x.VehicleBrand);

    private static ReceptionCustomerDto ToCustomerDto(Person person) => new(
        person.Id,
        person.DocumentType.Code,
        person.DocumentNumber,
        string.Join(' ', new[] { person.FirstName, person.MiddleName, person.LastName, person.SecondLastName }.Where(x => !string.IsNullOrWhiteSpace(x))),
        person.Emails.FirstOrDefault(x => x.IsPrimary) is { } email ? $"{email.EmailUser}@{email.EmailDomain.Domain}" : "",
        person.Phones.FirstOrDefault(x => x.IsPrimary)?.PhoneNumber ?? "",
        person.VehicleHistory.Count(x => x.EndDate == null),
        person.IsActive ? "Activo" : "Inactivo");

    private static ReceptionVehicleDto ToVehicleDto(Vehicle vehicle)
    {
        var owner = vehicle.OwnerHistory.Where(x => x.EndDate == null).OrderByDescending(x => x.StartDate).FirstOrDefault()?.Person;
        return new ReceptionVehicleDto(
            vehicle.Id,
            vehicle.Vin,
            vehicle.VehicleModel.VehicleBrand.BrandName,
            vehicle.VehicleModel.ModelName,
            vehicle.VehicleType.Name,
            vehicle.Year,
            vehicle.Color,
            vehicle.Mileage,
            owner?.Id,
            owner is null ? "Sin propietario" : $"{owner.FirstNames} {owner.LastNames}",
            vehicle.ServiceOrders.Count(x => x.OrderStatusId < 10),
            vehicle.IsActive);
    }

    private static ReceptionOwnerHistoryDto ToOwnerHistoryDto(Domain.Entities.VehicleOwnerHistory owner) => new(
        owner.Id,
        owner.VehicleId,
        owner.PersonId,
        $"{owner.Person.FirstNames} {owner.Person.LastNames}",
        owner.StartDate,
        owner.EndDate,
        owner.EndDate == null);

    private static ReceptionPaymentDto ToReceptionPaymentDto(Payment payment)
    {
        var vehicle = payment.Invoice.ServiceOrder.Vehicle;
        var vehicleName = $"{vehicle.VehicleModel.VehicleBrand.BrandName} {vehicle.VehicleModel.ModelName} {vehicle.Vin}";
        var client = payment.ClientPerson;
        return new ReceptionPaymentDto(
            payment.Id,
            payment.InvoiceId,
            payment.Invoice.ServiceOrderId,
            client?.Id,
            client is null ? "Cliente" : $"{client.FirstNames} {client.LastNames}",
            client?.DocumentNumber,
            vehicleName,
            payment.Amount,
            payment.Invoice.Total,
            Math.Max(0, payment.Invoice.Total - payment.Amount),
            payment.PaymentMethod.Name,
            payment.PaymentStatus.Name,
            payment.PaymentDate,
            payment.Reference ?? "");
    }

    private static (string User, string Domain)? SplitEmail(string email)
    {
        var parts = email.Trim().ToLowerInvariant().Split('@', 2);
        return parts.Length == 2 && !string.IsNullOrWhiteSpace(parts[0]) && !string.IsNullOrWhiteSpace(parts[1]) ? (parts[0], parts[1]) : null;
    }
}

public sealed record CreateReceptionCustomerRequest(int DocumentTypeId, string DocumentNumber, string FirstName, string? MiddleName, string LastName, string? SecondLastName, string? Email, string? Phone, int? PhoneCountryId);
public sealed record CreateReceptionVehicleRequest(int OwnerPersonId, int ModelId, int VehicleTypeId, string Vin, int Year, string? Color, int Mileage, DateTime? StartDate);
public sealed record TransferVehicleOwnerRequest(int NewOwnerPersonId, DateTime? TransferDate, string? Observation);
public sealed record ReceptionCustomerDto(int Id, string DocumentType, string DocumentNumber, string FullName, string PrimaryEmail, string PrimaryPhone, int VehiclesCount, string Status);
public sealed record ReceptionVehicleDto(int Id, string Vin, string Brand, string Model, string Type, int Year, string? Color, int Mileage, int? CurrentOwnerId, string CurrentOwner, int ActiveOrders, bool IsActive);
public sealed record ReceptionOwnerHistoryDto(int Id, int VehicleId, int PersonId, string Owner, DateTime StartDate, DateTime? EndDate, bool IsCurrent);
public sealed record ReceptionPaymentDto(int Id, int InvoiceId, int ServiceOrderId, int? ClientPersonId, string Customer, string? ClientDocument, string Vehicle, decimal Amount, decimal Total, decimal Balance, string Method, string Status, DateTime Date, string Reference);
