using Api.DTOs.OrderServiceParts;
using Application.UseCase.CommonCrud;
using Application.UseCase.OrderServiceParts;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.OrderServiceParts;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con OrderServiceParts.
public sealed class OrderServicePartsController : BaseApiController
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public OrderServicePartsController(ISender sender) : base(sender) { }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetEntitiesPaged<OrderServicePart>(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result.Items.Select(x => x.Adapt<OrderServicePartResponse>()).ToArray());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var entity = await Sender.Send(new GetEntityById<OrderServicePart>(id), ct);
        return Ok(entity.Adapt<OrderServicePartResponse>());
    }

    [Authorize(Policy = "MechanicOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderServicePartRequest request, CancellationToken ct)
    {
        var id = await Sender.Send(new CreateOrderServicePart(request.OrderServiceId, request.PartId, request.Quantity, request.AppliedUnitPrice), ct);
        return Created($"/api/orderserviceparts/{id}", new { id });
    }

    [Authorize(Policy = "MechanicOrAdmin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateOrderServicePartRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdateOrderServicePart(id, request.Quantity, request.AppliedUnitPrice, request.CustomerApproved), ct);
        return NoContent();
    }

    [Authorize(Policy = "MechanicOrAdmin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await Sender.Send(new DeleteOrderServicePart(id), ct);
        return NoContent();
    }
}
