using Api.Controllers;
using Application.UseCase.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Roles;

public sealed class RolesController : BaseApiController
{
    public RolesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetRolesPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetRoleById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRole command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/roles/{id}", new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateRoleRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdateRole(id, request.RoleName), ct);
        return NoContent();
    }

    public sealed record UpdateRoleRequest(string RoleName);
}
