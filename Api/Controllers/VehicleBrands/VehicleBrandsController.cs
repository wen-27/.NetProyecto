// Responsabilidad: Controlador HTTP que expone endpoints REST relacionados con VehicleBrands. Coordina validacion de entrada, autorizacion y delega la logica a Application/Infrastructure.
// Nota de mantenimiento: No debe contener reglas de negocio extensas; esas reglas pertenecen a Application o servicios especializados.
using Api.Controllers;
using Api.DTOs.VehicleBrands;
using Application.UseCase.VehicleBrands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.VehicleBrands;

public sealed class VehicleBrandsController : BaseApiController
{
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
