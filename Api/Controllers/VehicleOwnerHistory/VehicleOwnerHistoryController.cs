using Api.Controllers;
using Application.UseCase.VehicleOwnerHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.VehicleOwnerHistory;

public sealed class VehicleOwnerHistoryController : BaseApiController
{
    public VehicleOwnerHistoryController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetVehicleOwnerHistoryPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetVehicleOwnerHistoryById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVehicleOwner command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/vehicleownerhistory/{id}", new { id });
    }

    [HttpPatch("{vehicleId:int}/end")]
    public async Task<IActionResult> End(int vehicleId, EndVehicleOwnershipRequest request, CancellationToken ct)
    {
        await Sender.Send(new EndVehicleOwnership(vehicleId, request.EndDate), ct);
        return NoContent();
    }

    public sealed record EndVehicleOwnershipRequest(DateTime EndDate);
}
