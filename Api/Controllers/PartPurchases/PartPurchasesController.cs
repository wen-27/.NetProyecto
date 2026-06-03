using Api.Controllers;
using Application.UseCase.PartPurchases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.PartPurchases;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con PartPurchases.
public sealed class PartPurchasesController : BaseApiController
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public PartPurchasesController(ISender sender) : base(sender) { }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetPartPurchasesPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct) => Ok(await Sender.Send(new GetPartPurchaseById(id), ct));
}
