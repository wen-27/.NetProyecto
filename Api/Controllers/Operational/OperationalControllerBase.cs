using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Operational;

[ApiController]
public abstract class OperationalControllerBase : ControllerBase
{
    protected int CurrentPersonId()
    {
        var value = User.FindFirstValue("PersonId");
        if (!int.TryParse(value, out var personId))
        {
            throw new UnauthorizedAccessException("No se pudo identificar la persona autenticada.");
        }

        return personId;
    }

    protected async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> action)
    {
        try
        {
            return await action();
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
