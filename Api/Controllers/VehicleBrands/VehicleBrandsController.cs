using Api.Controllers;
using Api.DTOs.VehicleBrands;
using Application.UseCase.VehicleBrands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.VehicleBrands;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con VehicleBrands.
public sealed class VehicleBrandsController : BaseApiController
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public VehicleBrandsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetVehicleBrandsPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetVehicleBrandById(id), ct));
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateVehicleBrand command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/vehiclebrands/{id}", new { id });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateVehicleBrandRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdateVehicleBrand(id, request.BrandName), ct);
        return NoContent();
    }
}
