using Api.Controllers;
using Api.DTOs.ServiceTypes;
using Application.UseCase.ServiceTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.ServiceTypes;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con ServiceTypes.
public sealed class ServiceTypesController : BaseApiController
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public ServiceTypesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetServiceTypesPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetServiceTypeById(id), ct));
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceType command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/servicetypes/{id}", new { id });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateServiceTypeRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdateServiceType(id, request.Name, request.EstimatedDays), ct);
        return NoContent();
    }
}
