using Api.Controllers;
using Api.DTOs.DocumentTypes;
using Application.UseCase.DocumentTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.DocumentTypes;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con DocumentTypes.
public sealed class DocumentTypesController : BaseApiController
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public DocumentTypesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetDocumentTypesPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetDocumentTypeById(id), ct));
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateDocumentType command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/documenttypes/{id}", new { id });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateDocumentTypeRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdateDocumentType(id, request.Code, request.Name), ct);
        return NoContent();
    }
}
