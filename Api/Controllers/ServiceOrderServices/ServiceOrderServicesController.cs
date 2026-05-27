using Api.Controllers;
using Application.UseCase.ServiceOrderServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.ServiceOrderServices;

public sealed class ServiceOrderServicesController : BaseApiController
{
    public ServiceOrderServicesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetServiceOrderServicesPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetServiceOrderServiceById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddServiceToOrder command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/serviceorderservices/{id}", new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateServiceOrderServiceRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdateServiceOrderService(id, request.ServiceTypeId, request.MechanicId, request.Description, request.LaborCost), ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id, CancellationToken ct)
    {
        await Sender.Send(new RemoveServiceFromOrder(id), ct);
        return NoContent();
    }

    public sealed record UpdateServiceOrderServiceRequest(int ServiceTypeId, int MechanicId, string? Description, decimal LaborCost);
}
