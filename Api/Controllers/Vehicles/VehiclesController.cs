using Api.Controllers;
using Application.UseCase.Vehicles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Vehicles;

public sealed class VehiclesController : BaseApiController
{
    public VehiclesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetVehiclesPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetVehicleById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVehicle command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/vehicles/{id}", new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateVehicleRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdateVehicle(id, request.ModelId, request.VehicleTypeId, request.Vin, request.Year, request.Mileage), ct);
        return NoContent();
    }

    public sealed record UpdateVehicleRequest(int ModelId, int VehicleTypeId, string Vin, int Year, int Mileage);
}
