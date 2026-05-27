using Api.Controllers;
using Api.DTOs.VehicleModels;
using Application.UseCase.VehicleModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.VehicleModels;

public sealed class VehicleModelsController : BaseApiController
{
    public VehicleModelsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetVehicleModelsPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetVehicleModelById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVehicleModel command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/vehiclemodels/{id}", new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateVehicleModelRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdateVehicleModel(id, request.BrandId, request.ModelName), ct);
        return NoContent();
    }
}
