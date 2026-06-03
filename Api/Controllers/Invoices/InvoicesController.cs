// Responsabilidad: Controlador HTTP que expone endpoints REST relacionados con Invoices. Coordina validacion de entrada, autorizacion y delega la logica a Application/Infrastructure.
// Nota de mantenimiento: No debe contener reglas de negocio extensas; esas reglas pertenecen a Application o servicios especializados.
using System.Security.Claims;
using Application.Abstractions.OperationalWorkflow;
using Application.UseCase.Invoices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Invoices;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class InvoicesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IOperationalWorkflowService _workflow;

    public InvoicesController(ISender sender, IOperationalWorkflowService workflow)
    {
        _sender = sender;
        _workflow = workflow;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        if (User.IsInRole("Client"))
        {
            return Ok(await _workflow.GetClientInvoicesAsync(CurrentPersonId(), ct));
        }

        if (User.IsInRole("Receptionist") || User.IsInRole("Admin"))
        {
            return Ok(await _workflow.GetReceptionInvoicesAsync(ct));
        }

        return Forbid();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        if (User.IsInRole("Client"))
        {
            var invoices = await _workflow.GetClientInvoicesAsync(CurrentPersonId(), ct);
            var invoice = invoices.FirstOrDefault(x => x.Id == id);
            return invoice is null ? NotFound(new { message = "La factura no existe." }) : Ok(invoice);
        }

        if (User.IsInRole("Receptionist") || User.IsInRole("Admin"))
        {
            var invoices = await _workflow.GetReceptionInvoicesAsync(ct);
            var invoice = invoices.FirstOrDefault(x => x.Id == id);
            return invoice is null ? NotFound(new { message = "La factura no existe." }) : Ok(invoice);
        }

        return Forbid();
    }

    [Authorize(Policy = "MechanicOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> Generate(GenerateInvoice command, CancellationToken ct)
    {
        var id = await _sender.Send(command, ct);
        return Created($"/api/invoices/{id}", new { id });
    }

    private int CurrentPersonId()
    {
        var value = User.FindFirstValue("PersonId");
        if (!int.TryParse(value, out var personId))
        {
            throw new UnauthorizedAccessException("No se pudo identificar la persona autenticada.");
        }

        return personId;
    }
}
