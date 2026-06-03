using Api.Controllers;
using Application.UseCase.UserRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UserRoles;

[Authorize(Policy = "AdminOnly")]
// Controlador encargado de exponer por HTTP las operaciones relacionadas con UserRoles.
public sealed class UserRolesController : BaseApiController
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public UserRolesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetUserRolesPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{userId:int}/{roleId:int}")]
    public async Task<IActionResult> GetByIds(int userId, int roleId, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetUserRoleByIds(userId, roleId), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Assign(AssignUserRole command, CancellationToken ct)
    {
        await Sender.Send(command, ct);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Remove([FromQuery] int userId, [FromQuery] int roleId, CancellationToken ct)
    {
        await Sender.Send(new RemoveUserRole(userId, roleId), ct);
        return NoContent();
    }
}
