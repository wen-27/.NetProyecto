// Responsabilidad: Controlador HTTP que expone endpoints REST relacionados con BaseApi. Coordina validacion de entrada, autorizacion y delega la logica a Application/Infrastructure.
// Nota de mantenimiento: No debe contener reglas de negocio extensas; esas reglas pertenecen a Application o servicios especializados.
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize(Policy = "InternalStaff")]
[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected BaseApiController(ISender sender)
    {
        Sender = sender;
    }

    protected ISender Sender { get; }
}
