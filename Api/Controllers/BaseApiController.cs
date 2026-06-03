using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize(Policy = "InternalStaff")]
[ApiController]
[Route("api/[controller]")]
// Controlador encargado de exponer por HTTP las operaciones relacionadas con BaseApi.
public abstract class BaseApiController : ControllerBase
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    protected BaseApiController(ISender sender)
    {
        Sender = sender;
    }

    protected ISender Sender { get; }
}
