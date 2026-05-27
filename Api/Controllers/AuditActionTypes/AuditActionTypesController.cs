using Api.Controllers;
using Api.DTOs.AuditActionTypes;
using Application.UseCase.AuditActionTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AuditActionTypes;

public sealed class AuditActionTypesController : BaseApiController
{
    public AuditActionTypesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetAuditActionTypesPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetAuditActionTypeById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAuditActionType command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/auditactiontypes/{id}", new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateAuditActionTypeRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdateAuditActionType(id, request.Name), ct);
        return NoContent();
    }
}
