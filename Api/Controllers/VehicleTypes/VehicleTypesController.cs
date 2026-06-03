using Api.Controllers;
using Application.UseCase.VehicleTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.VehicleTypes;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con VehicleTypes.
public sealed class VehicleTypesController : BaseApiController
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public VehicleTypesController(ISender sender) : base(sender) { }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetVehicleTypesPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct) => Ok(await Sender.Send(new GetVehicleTypeById(id), ct));
}
