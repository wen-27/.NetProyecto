using Api.Controllers;
using Api.DTOs.VehicleOwnerHistory;
using Application.UseCase.VehicleOwnerHistory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.VehicleOwnerHistory;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con VehicleOwnerHistory.
public sealed class VehicleOwnerHistoryController : BaseApiController
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
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

    [Authorize(Policy = "ReceptionistOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVehicleOwner command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/vehicleownerhistory/{id}", new { id });
    }

    [Authorize(Policy = "ReceptionistOrAdmin")]
    [HttpPatch("{vehicleId:int}/end")]
    public async Task<IActionResult> End(int vehicleId, EndVehicleOwnershipRequest request, CancellationToken ct)
    {
        await Sender.Send(new EndVehicleOwnership(vehicleId, request.EndDate), ct);
        return NoContent();
    }
}
