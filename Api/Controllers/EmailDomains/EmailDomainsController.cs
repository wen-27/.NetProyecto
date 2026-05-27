using Api.Controllers;
using Api.DTOs.EmailDomains;
using Application.UseCase.EmailDomains;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.EmailDomains;

public sealed class EmailDomainsController : BaseApiController
{
    public EmailDomainsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetEmailDomainsPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetEmailDomainById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmailDomain command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/emaildomains/{id}", new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateEmailDomainRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdateEmailDomain(id, request.Domain), ct);
        return NoContent();
    }
}
