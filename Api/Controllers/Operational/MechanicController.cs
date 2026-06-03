using Application.Abstractions.OperationalWorkflow;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Operational;

[Route("api/mechanic")]
[Authorize(Roles = "Mechanic,Admin")]
// Controlador encargado de exponer por HTTP las operaciones relacionadas con Mechanic.
public sealed class MechanicController : OperationalControllerBase
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    private readonly IOperationalWorkflowService _workflow;

    public MechanicController(IOperationalWorkflowService workflow)
    {
        _workflow = workflow;
    }

    [HttpGet("orders")]
    public Task<IActionResult> GetOrders(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetMechanicOrdersAsync(CurrentPersonId(), ct)));

    [HttpGet("orders/{orderId:int}")]
    public Task<IActionResult> GetOrder(int orderId, CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetMechanicOrderAsync(CurrentPersonId(), orderId, ct)));

    [HttpGet("requests")]
    public Task<IActionResult> GetRequests(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetMechanicRequestsAsync(CurrentPersonId(), ct)));

    [HttpGet("diagnostics")]
    public Task<IActionResult> GetDiagnostics(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetMechanicDiagnosticsAsync(CurrentPersonId(), ct)));

    [HttpPost("orders/{orderId:int}/diagnostics")]
    public Task<IActionResult> SubmitDiagnostic(int orderId, CreateMechanicDiagnosticDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Created($"/api/mechanic/diagnostics", await _workflow.SubmitMechanicDiagnosticAsync(CurrentPersonId(), orderId, dto, ct)));

    [HttpPost("orders/{orderId:int}/additional-requests")]
    public Task<IActionResult> CreateAdditionalRequest(int orderId, CreateAdditionalRequestDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Created($"/api/mechanic/requests", await _workflow.CreateAdditionalRequestAsync(CurrentPersonId(), orderId, dto, ct)));

    [HttpPost("orders/{orderId:int}/work")]
    public Task<IActionResult> RecordWork(int orderId, RecordMechanicWorkDto dto, CancellationToken ct) =>
        ExecuteAsync(async () =>
        {
            await _workflow.RecordMechanicWorkAsync(CurrentPersonId(), orderId, dto, ct);
            return NoContent();
        });

    [HttpPost("orders/{orderId:int}/complete")]
    public Task<IActionResult> CompleteOrder(int orderId, RecordMechanicWorkDto dto, CancellationToken ct) =>
        ExecuteAsync(async () =>
        {
            await _workflow.CompleteMechanicOrderAsync(CurrentPersonId(), orderId, dto, ct);
            return NoContent();
        });

    [HttpPatch("order-services/{orderServiceId:int}/status")]
    public Task<IActionResult> UpdateOrderServiceStatus(int orderServiceId, UpdateMechanicOrderServiceStatusDto dto, CancellationToken ct) =>
        ExecuteAsync(async () =>
        {
            await _workflow.UpdateMechanicOrderServiceStatusAsync(CurrentPersonId(), orderServiceId, dto, ct);
            return NoContent();
        });
}
