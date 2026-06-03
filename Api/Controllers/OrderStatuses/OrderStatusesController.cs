using Api.Controllers;
using Api.DTOs.OrderStatuses;
using Application.UseCase.OrderStatuses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.OrderStatuses;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con OrderStatuses.
public sealed class OrderStatusesController : BaseApiController
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public OrderStatusesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetOrderStatusesPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetOrderStatusById(id), ct));
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderStatus command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/orderstatuses/{id}", new { id });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateOrderStatusRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdateOrderStatus(id, request.Name), ct);
        return NoContent();
    }
}
