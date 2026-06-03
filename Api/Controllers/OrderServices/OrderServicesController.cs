using Api.DTOs.OrderServices;
using Application.Abstractions;
using Application.UseCase.CommonCrud;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.OrderServices;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con OrderServices.
public sealed class OrderServicesController : BaseApiController
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    private readonly IOrderServiceRepository _repository;

    public OrderServicesController(ISender sender, IOrderServiceRepository repository) : base(sender)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery(Name = "pageNumber")] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        CancellationToken ct = default)
    {
        var page = pageNumber < 1 ? 1 : pageNumber;
        var size = pageSize < 1 ? 10 : pageSize;
        var items = await _repository.GetPagedAsync(page, size, search, ct);
        var total = await _repository.CountAsync(search, ct);
        Response.Headers["X-Total-Count"] = total.ToString();

        return Ok(new
        {
            items = items.Select(x => x.Adapt<OrderServiceResponse>()).ToArray(),
            TotalCount = total,
            Page = page,
            PageSize = size,
            TotalPages = (int)Math.Ceiling(total / (double)size),
            HasPreviousPage = page > 1,
            HasNextPage = page * size < total
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(id, ct);
        return entity is null ? NotFound() : Ok(entity.Adapt<OrderServiceResponse>());
    }

    [HttpGet("by-order/{serviceOrderId:int}")]
    public async Task<IActionResult> GetByServiceOrderId(int serviceOrderId, CancellationToken ct)
    {
        var services = await _repository.GetByServiceOrderIdAsync(serviceOrderId, ct);
        return Ok(services.Select(x => x.Adapt<OrderServiceResponse>()).ToArray());
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderServiceRequest request, CancellationToken ct)
    {
        var entity = request.Adapt<OrderService>();
        var id = await Sender.Send(new CreateEntity<OrderService>(entity), ct);
        return Created($"{Request.Path}/{id}", new { id });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateOrderServiceRequest request, CancellationToken ct)
    {
        var entity = request.Adapt<OrderService>();
        await Sender.Send(new UpdateEntity<OrderService>(id, entity), ct);
        return NoContent();
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await Sender.Send(new DeleteEntity<OrderService>(id), ct);
        return NoContent();
    }
}
