using Api.Controllers;
using Application.UseCase.Audits;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Audits;

public sealed class AuditsController : BaseApiController
{
    public AuditsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetAuditsPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetAuditById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterAudit command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/audits/{id}", new { id });
    }
}
