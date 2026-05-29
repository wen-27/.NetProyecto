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
public sealed class ServiceOrdersController : BaseApiController
{
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
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
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

    [Authorize(Policy = "WorkshopChiefOrAdmin")]
    [HttpPost("diagnostic")]
    public async Task<IActionResult> CreateDiagnosticOrder(CreateDiagnosticServiceOrderRequest request, CancellationToken ct)
    {
        if (request.ClientPersonId <= 0 || request.VehicleId <= 0)
        {
            return BadRequest("Debe seleccionar un cliente y un vehículo asociado.");
        }

        if (request.Mileage < 0 ||
            string.IsNullOrWhiteSpace(request.ProblemDescription) ||
            string.IsNullOrWhiteSpace(request.Observations) ||
            string.IsNullOrWhiteSpace(request.Checklist.FuelLevel) ||
            string.IsNullOrWhiteSpace(request.Checklist.ObjectsInsideVehicle) ||
            string.IsNullOrWhiteSpace(request.ServiceAssignment.Observation))
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

        var serviceTypeExists = await _dbContext.ServiceTypes.AnyAsync(x => x.Id == request.ServiceAssignment.ServiceTypeId, ct);
        if (!serviceTypeExists)
        {
            return BadRequest("El servicio seleccionado no existe.");
        }

        var mechanicCanHandleSpecialty = await _dbContext.MechanicSpecialtyAssignments.AnyAsync(x =>
            x.PersonId == request.ServiceAssignment.MechanicPersonId &&
            x.SpecialtyId == request.ServiceAssignment.SpecialtyId,
            ct);
        if (!mechanicCanHandleSpecialty)
        {
            return BadRequest("El mecánico seleccionado no tiene asignada esa especialidad.");
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);

        vehicle.Mileage = request.Mileage;
        var serviceOrder = new ServiceOrder
        {
            VehicleId = request.VehicleId,
            OrderStatusId = (int)ServiceOrderStatus.Assigned,
            EntryDate = request.EntryDate,
            EstimatedDeliveryDate = request.EstimatedDeliveryDate,
            GeneralDescription = $"Problema reportado: {request.ProblemDescription}\nObservaciones: {request.Observations}",
            EstimatedTotal = request.ServiceAssignment.LaborCost
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

        var orderService = new OrderService
        {
            ServiceOrderId = serviceOrder.Id,
            ServiceTypeId = request.ServiceAssignment.ServiceTypeId,
            Description = request.ServiceAssignment.Observation,
            LaborCost = request.ServiceAssignment.LaborCost,
            Price = 0,
            Status = OrderServiceStatus.Pending
        };

        await _dbContext.OrderServices.AddAsync(orderService, ct);
        await _dbContext.SaveChangesAsync(ct);

        await _dbContext.MechanicAssignments.AddAsync(new MechanicAssignment
        {
            OrderServiceId = orderService.Id,
            MechanicPersonId = request.ServiceAssignment.MechanicPersonId,
            SpecialtyId = request.ServiceAssignment.SpecialtyId
        }, ct);

        await _dbContext.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);

        return Created($"/api/serviceorders/{serviceOrder.Id}", new { id = serviceOrder.Id });
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
