using Api.Controllers;
using Api.DTOs.ServiceOrders;
using Application.UseCase.ServiceOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.ServiceOrders;

public sealed class ServiceOrdersController : BaseApiController
{
    public ServiceOrdersController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetServiceOrdersPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetServiceOrderById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceOrder command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/serviceorders/{id}", new { id });
    }

    [HttpPatch("{id:int}/work")]
    public async Task<IActionResult> RecordWork(int id, RecordServiceOrderWorkRequest request, CancellationToken ct)
    {
        await Sender.Send(new RecordServiceOrderWork(id, request.WorkPerformed), ct);
        return NoContent();
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> ChangeStatus(int id, ChangeServiceOrderStatusRequest request, CancellationToken ct)
    {
        await Sender.Send(new ChangeServiceOrderStatus(id, request.OrderStatusId, request.UserId, request.Observation), ct);
        return NoContent();
    }
}
