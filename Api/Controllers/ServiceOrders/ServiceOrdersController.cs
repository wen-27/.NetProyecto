using System.Security.Claims;
using Api.Controllers;
using Api.DTOs.OrderServices;
using Api.DTOs.ServiceOrders;
using Application.Abstractions;
using Application.UseCase.ServiceOrders;
using Domain.Entities;
using Domain.Enums;
using Domain.Enums.OrderStatus;
using Infrastructure.Context;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.ServiceOrders;

[EnableRateLimiting("service-orders")]
// Controlador encargado de exponer por HTTP las operaciones relacionadas con ServiceOrders.
public sealed class ServiceOrdersController : BaseApiController
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    private readonly IOrderServiceRepository _orderServiceRepository;
    private readonly AppDbContext _dbContext;

    public ServiceOrdersController(ISender sender, IOrderServiceRepository orderServiceRepository, AppDbContext dbContext) : base(sender)
    {
        _orderServiceRepository = orderServiceRepository;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery(Name = "pageNumber")] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] int? clientPersonId = null,
        [FromQuery] string? vin = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null,
        [FromQuery] int? statusId = null,
        [FromQuery] int? mechanicPersonId = null,
        CancellationToken ct = default)
    {
        var result = await Sender.Send(
            new GetServiceOrdersPaged(pageNumber, pageSize, search, clientPersonId, vin, fromDate, toDate, statusId, mechanicPersonId),
            ct);
        var orderIds = result.Items.Select(order => order.Id).ToList();
        if (orderIds.Count == 0)
        {
            Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
            return Ok(new
            {
                items = Array.Empty<object>(),
                result.TotalCount,
                result.Page,
                result.PageSize,
                result.TotalPages,
                result.HasPreviousPage,
                result.HasNextPage
            });
        }

        var assignmentRows = await _dbContext.MechanicAssignments
            .AsNoTracking()
            .Where(assignment => orderIds.Contains(assignment.OrderService.ServiceOrderId))
            .Select(assignment => new
            {
                assignment.OrderService.ServiceOrderId,
                ServiceName = assignment.OrderService.WorkshopService != null
                    ? assignment.OrderService.WorkshopService.Name
                    : assignment.OrderService.Description ?? assignment.OrderService.ServiceType.Name,
                assignment.MechanicPerson.FirstName,
                assignment.MechanicPerson.MiddleName,
                assignment.MechanicPerson.LastName,
                assignment.MechanicPerson.SecondLastName
            })
            .ToListAsync(ct);

        var assignmentLookup = assignmentRows
            .GroupBy(row => row.ServiceOrderId)
            .ToDictionary(group => group.Key, group => new
            {
                services = group.Select(row => row.ServiceName).Where(value => !string.IsNullOrWhiteSpace(value)).Distinct().ToArray(),
                assignedMechanics = group
                    .Select(row => string.Join(' ', new[] { row.FirstName, row.MiddleName, row.LastName, row.SecondLastName }.Where(value => !string.IsNullOrWhiteSpace(value))))
                    .Where(value => !string.IsNullOrWhiteSpace(value))
                    .Distinct()
                    .ToArray()
            });

        var items = result.Items.Select(order =>
        {
            assignmentLookup.TryGetValue(order.Id, out var assignments);
            var mechanics = assignments?.assignedMechanics ?? Array.Empty<string>();
            return new
            {
                order.Id,
                order.VehicleId,
                order.OrderStatusId,
                order.EntryDate,
                order.EstimatedDeliveryDate,
                order.WorkPerformed,
                order.EstimatedTotal,
                order.Customer,
                order.Vehicle,
                order.Status,
                order.GeneralDescription,
                mechanic = mechanics.Length == 0 ? "Sin asignar" : string.Join(", ", mechanics),
                services = assignments?.services ?? Array.Empty<string>(),
                assignedMechanics = mechanics
            };
        }).ToArray();

        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(new
        {
            items,
            result.TotalCount,
            result.Page,
            result.PageSize,
            result.TotalPages,
            result.HasPreviousPage,
            result.HasNextPage
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetServiceOrderById(id), ct));
    }

    [HttpGet("{id:int}/services")]
    public async Task<IActionResult> GetServices(int id, CancellationToken ct)
    {
        var services = await _orderServiceRepository.GetByServiceOrderIdAsync(id, ct);
        return Ok(services.Select(x => x.Adapt<OrderServiceResponse>()).ToArray());
    }

    [Authorize(Policy = "InternalStaff")]
    [HttpPost("empty")]
    public async Task<IActionResult> CreateEmptyOrder(CreateEmptyServiceOrderRequest request, CancellationToken ct)
    {
        if (request.ClientPersonId <= 0 || request.VehicleId <= 0)
        {
            return BadRequest("Debe seleccionar un cliente y un vehículo asociado.");
        }

        var vehicle = await _dbContext.Vehicles
            .Include(x => x.OwnerHistory)
            .FirstOrDefaultAsync(x => x.Id == request.VehicleId && x.IsActive, ct);
        if (vehicle is null)
        {
            return NotFound("El vehículo no existe o no está activo.");
        }

        var belongsToClient = vehicle.OwnerHistory.Any(x => x.PersonId == request.ClientPersonId && x.EndDate == null);
        if (!belongsToClient)
        {
            return BadRequest("El vehículo seleccionado no está asociado actualmente a ese cliente.");
        }

        var hasActiveOrder = await _dbContext.ServiceOrders
            .Include(x => x.OrderStatus)
            .AnyAsync(x =>
                x.VehicleId == request.VehicleId &&
                x.OrderStatus.Name != "Delivered" &&
                x.OrderStatus.Name != "Cancelled" &&
                x.OrderStatus.Name != "Voided",
                ct);
        if (hasActiveOrder)
        {
            return BadRequest("El vehículo ya tiene una orden activa.");
        }

        var serviceOrder = new ServiceOrder
        {
            VehicleId = request.VehicleId,
            OrderStatusId = (int)ServiceOrderStatus.Created,
            EntryDate = DateTime.UtcNow,
            EstimatedTotal = 0
        };

        await _dbContext.ServiceOrders.AddAsync(serviceOrder, ct);
        await _dbContext.SaveChangesAsync(ct);

        return Created($"/api/serviceorders/{serviceOrder.Id}", new { id = serviceOrder.Id });
    }

    [Authorize(Policy = "InternalStaff")]
    [HttpPost("diagnostic")]
    public async Task<IActionResult> CreateDiagnosticOrder(CreateDiagnosticServiceOrderRequest request, CancellationToken ct)
    {
        if (request.ClientPersonId <= 0 || request.VehicleId <= 0)
        {
            return BadRequest("Debe seleccionar un cliente y un vehículo asociado.");
        }

        var serviceAssignments = (request.ServiceAssignments is { Count: > 0 }
                ? request.ServiceAssignments
                : request.ServiceAssignment is null
                    ? Array.Empty<DiagnosticServiceAssignmentRequest>()
                    : new[] { request.ServiceAssignment })
            .ToList();

        if (request.Mileage < 0 ||
            string.IsNullOrWhiteSpace(request.ProblemDescription) ||
            string.IsNullOrWhiteSpace(request.Observations) ||
            string.IsNullOrWhiteSpace(request.Checklist.FuelLevel) ||
            string.IsNullOrWhiteSpace(request.Checklist.ObjectsInsideVehicle) ||
            serviceAssignments.Count == 0 ||
            serviceAssignments.Any(x =>
                x.ServiceTypeId <= 0 ||
                x.SpecialtyId <= 0 ||
                x.MechanicPersonId <= 0 ||
                string.IsNullOrWhiteSpace(x.Observation)))
        {
            return BadRequest("Debe completar todos los datos de ingreso, checklist y asignación.");
        }

        if (request.EstimatedDeliveryDate <= request.EntryDate)
        {
            return BadRequest("La fecha estimada de entrega debe ser posterior a la fecha de ingreso.");
        }

        var vehicle = await _dbContext.Vehicles
            .Include(x => x.OwnerHistory)
            .FirstOrDefaultAsync(x => x.Id == request.VehicleId && x.IsActive, ct);
        if (vehicle is null)
        {
            return NotFound("El vehículo no existe o no está activo.");
        }

        var belongsToClient = vehicle.OwnerHistory.Any(x => x.PersonId == request.ClientPersonId && x.EndDate == null);
        if (!belongsToClient)
        {
            return BadRequest("El vehículo seleccionado no está asociado actualmente a ese cliente.");
        }

        var hasActiveOrder = await _dbContext.ServiceOrders
            .Include(x => x.OrderStatus)
            .AnyAsync(x =>
                x.VehicleId == request.VehicleId &&
                x.OrderStatus.Name != "Delivered" &&
                x.OrderStatus.Name != "Cancelled" &&
                x.OrderStatus.Name != "Voided",
                ct);
        if (hasActiveOrder)
        {
            return BadRequest("El vehículo ya tiene una orden activa.");
        }

        var serviceTypeIds = serviceAssignments.Select(x => x.ServiceTypeId).Distinct().ToArray();
        foreach (var serviceTypeId in serviceTypeIds)
        {
            var serviceTypeExists = await _dbContext.ServiceTypes.AnyAsync(x => x.Id == serviceTypeId, ct);
            if (!serviceTypeExists)
            {
                return BadRequest("Uno o más tipos de servicio no existen.");
            }
        }

        var workshopServiceIds = serviceAssignments
            .Where(x => x.WorkshopServiceId.HasValue)
            .Select(x => x.WorkshopServiceId!.Value)
            .Distinct()
            .ToArray();
        var workshopServices = new Dictionary<int, WorkshopService>();
        foreach (var workshopServiceId in workshopServiceIds)
        {
            var workshopService = await _dbContext.WorkshopServices.FirstOrDefaultAsync(x =>
                x.Id == workshopServiceId &&
                x.Status == WorkshopServiceStatus.Active,
                ct);
            if (workshopService is null)
            {
                return BadRequest("Uno o más servicios del taller no existen o están inactivos.");
            }

            workshopServices[workshopServiceId] = workshopService;
        }

        foreach (var assignment in serviceAssignments)
        {
            var mechanicCanHandleSpecialty = await _dbContext.MechanicSpecialtyAssignments.AnyAsync(x =>
                x.PersonId == assignment.MechanicPersonId &&
                x.SpecialtyId == assignment.SpecialtyId,
                ct);
            if (!mechanicCanHandleSpecialty)
            {
                return BadRequest("Uno o más mecánicos seleccionados no tienen asignada la especialidad requerida.");
            }
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);

        vehicle.Mileage = request.Mileage;
        var estimatedTotal = serviceAssignments.Sum(assignment =>
        {
            var workshopService = assignment.WorkshopServiceId.HasValue
                ? workshopServices[assignment.WorkshopServiceId.Value]
                : null;

            return workshopService?.FinalPrice ?? assignment.LaborCost;
        });

        var serviceOrder = new ServiceOrder
        {
            VehicleId = request.VehicleId,
            OrderStatusId = (int)ServiceOrderStatus.Assigned,
            EntryDate = request.EntryDate,
            EstimatedDeliveryDate = request.EstimatedDeliveryDate,
            GeneralDescription = $"Problema reportado: {request.ProblemDescription}\nObservaciones: {request.Observations}",
            EstimatedTotal = estimatedTotal
        };

        await _dbContext.ServiceOrders.AddAsync(serviceOrder, ct);
        await _dbContext.SaveChangesAsync(ct);

        await _dbContext.VehicleEntryInventory.AddAsync(new Domain.Entities.VehicleEntryInventory
        {
            ServiceOrderId = serviceOrder.Id,
            HasScratches = request.Checklist.ScratchesOrDents,
            ScratchesDescription = request.Checklist.ScratchesOrDents ? request.Checklist.Notes : null,
            HasToolbox = request.Checklist.Tools,
            ToolboxDescription = request.Checklist.Tools ? "Herramientas entregadas" : null,
            OwnershipCardDelivered = request.Checklist.Documents,
            Observations =
                $"Luces: {(request.Checklist.Lights ? "OK" : "Pendiente")}\n" +
                $"Llantas: {(request.Checklist.Tires ? "OK" : "Pendiente")}\n" +
                $"Espejos: {(request.Checklist.Mirrors ? "OK" : "Pendiente")}\n" +
                $"Documentos: {(request.Checklist.Documents ? "Entregados" : "No entregados")}\n" +
                $"Herramientas: {(request.Checklist.Tools ? "Sí" : "No")}\n" +
                $"Rayones/golpes: {(request.Checklist.ScratchesOrDents ? "Sí" : "No")}\n" +
                $"Nivel de combustible: {request.Checklist.FuelLevel}\n" +
                $"Objetos dentro del vehículo: {request.Checklist.ObjectsInsideVehicle}\n" +
                $"Notas: {request.Checklist.Notes ?? "Sin notas"}",
            RegisteredAt = DateTime.UtcNow
        }, ct);

        foreach (var assignment in serviceAssignments)
        {
            var workshopService = assignment.WorkshopServiceId.HasValue
                ? workshopServices[assignment.WorkshopServiceId.Value]
                : null;

            var orderService = new OrderService
            {
                ServiceOrderId = serviceOrder.Id,
                ServiceTypeId = assignment.ServiceTypeId,
                WorkshopServiceId = assignment.WorkshopServiceId,
                Description = assignment.Observation,
                LaborCost = workshopService?.LaborAmount ?? assignment.LaborCost,
                Price = workshopService?.PartsSubtotal ?? 0,
                Status = OrderServiceStatus.Pending
            };

            await _dbContext.OrderServices.AddAsync(orderService, ct);
            await _dbContext.SaveChangesAsync(ct);

            await _dbContext.MechanicAssignments.AddAsync(new MechanicAssignment
            {
                OrderServiceId = orderService.Id,
                MechanicPersonId = assignment.MechanicPersonId,
                SpecialtyId = assignment.SpecialtyId
            }, ct);
        }

        var diagnosticMechanicPersonId = CurrentPersonIdOrDefault() ?? serviceAssignments[0].MechanicPersonId;
        await _dbContext.MechanicDiagnostics.AddAsync(new MechanicDiagnostic
        {
            ServiceOrderId = serviceOrder.Id,
            MechanicPersonId = diagnosticMechanicPersonId,
            Status = MechanicDiagnosticStatus.PendingWorkshopChiefApproval,
            Findings = request.ProblemDescription.Trim(),
            RecommendedWork = request.Observations.Trim(),
            SubmittedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        }, ct);

        await _dbContext.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);

        return Created($"/api/serviceorders/{serviceOrder.Id}", new { id = serviceOrder.Id });
    }

    private int? CurrentPersonIdOrDefault()
    {
        var value = User.FindFirstValue("PersonId");
        return int.TryParse(value, out var personId) ? personId : null;
    }

    [Authorize(Policy = "InternalStaff")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceOrder command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/serviceorders/{id}", new { id });
    }

    [Authorize(Policy = "MechanicOrAdmin")]
    [HttpPatch("{id:int}/work")]
    public async Task<IActionResult> RecordWork(int id, RecordServiceOrderWorkRequest request, CancellationToken ct)
    {
        await Sender.Send(new RecordServiceOrderWork(id, request.WorkPerformed), ct);
        return NoContent();
    }

    [Authorize(Policy = "MechanicOrAdmin")]
    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> ChangeStatus(int id, ChangeServiceOrderStatusRequest request, CancellationToken ct)
    {
        await Sender.Send(new ChangeServiceOrderStatus(id, request.OrderStatusId, request.UserId, request.Observation), ct);
        return NoContent();
    }
}
