using Api.Controllers;
using Application.UseCase.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Users;

public sealed class UsersController : BaseApiController
{
    public UsersController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetUsersPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetUserById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUser command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/users/{id}", new { id });
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> ChangeStatus(int id, ChangeUserStatusRequest request, CancellationToken ct)
    {
        await Sender.Send(new ChangeUserStatus(id, request.Status), ct);
        return NoContent();
    }

    public sealed record ChangeUserStatusRequest(bool Status);
}
