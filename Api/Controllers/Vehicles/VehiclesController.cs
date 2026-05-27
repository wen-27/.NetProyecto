using Api.Controllers;
using Api.DTOs.Vehicles;
using Application.UseCase.Vehicles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Vehicles;

public sealed class VehiclesController : BaseApiController
{
    public VehiclesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery(Name = "pageNumber")] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] string? vin = null,
        [FromQuery] int? clientPersonId = null,
        CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetVehiclesPaged(pageNumber, pageSize, search, vin, clientPersonId), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetVehicleById(id), ct));
    }

    [Authorize(Policy = "ReceptionistOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateVehicle command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/vehicles/{id}", new { id });
    }

    [Authorize(Policy = "ReceptionistOrAdmin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateVehicleRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdateVehicle(id, request.ModelId, request.VehicleTypeId, request.Vin, request.Year, request.Color, request.Mileage, request.IsActive), ct);
        return NoContent();
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await Sender.Send(new DeleteVehicle(id), ct);
        return NoContent();
    }
}
