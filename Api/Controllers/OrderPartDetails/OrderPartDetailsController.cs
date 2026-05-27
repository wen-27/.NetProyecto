using Api.Controllers;
using Application.UseCase.OrderPartDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.OrderPartDetails;

public sealed class OrderPartDetailsController : BaseApiController
{
    public OrderPartDetailsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetOrderPartDetailsPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetOrderPartDetailById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddPartToServiceOrder command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/orderpartdetails/{id}", new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateServiceOrderPartRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdateServiceOrderPart(id, request.Quantity, request.AppliedUnitPrice), ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id, CancellationToken ct)
    {
        await Sender.Send(new RemovePartFromServiceOrder(id), ct);
        return NoContent();
    }

    public sealed record UpdateServiceOrderPartRequest(int Quantity, decimal AppliedUnitPrice);
}
