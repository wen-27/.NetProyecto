using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Operational;

[ApiController]
// Controlador encargado de exponer por HTTP las operaciones relacionadas con OperationalControllerBase.
public abstract class OperationalControllerBase : ControllerBase
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
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
            return StatusCode(StatusCodes.Status403Forbidden, new { message = ex.Message });
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
