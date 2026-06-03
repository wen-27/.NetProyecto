// Responsabilidad: Controlador HTTP que expone endpoints REST relacionados con PersonEmails. Coordina validacion de entrada, autorizacion y delega la logica a Application/Infrastructure.
// Nota de mantenimiento: No debe contener reglas de negocio extensas; esas reglas pertenecen a Application o servicios especializados.
using Api.Controllers;
using Api.DTOs.PersonEmails;
using Application.UseCase.PersonEmails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.PersonEmails;

public sealed class PersonEmailsController : BaseApiController
{
    public PersonEmailsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetPersonEmailsPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetPersonEmailById(id), ct));
    }

    [Authorize(Policy = "ReceptionistOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> Add(AddPersonEmail command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/personemails/{id}", new { id });
    }

    [Authorize(Policy = "ReceptionistOrAdmin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdatePersonEmailRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdatePersonEmail(id, request.EmailDomainId, request.EmailUser, request.IsPrimary), ct);
        return NoContent();
    }
}
