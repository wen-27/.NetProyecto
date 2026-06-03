// Responsabilidad: Controlador HTTP que expone endpoints REST relacionados con WorkshopChief. Coordina validacion de entrada, autorizacion y delega la logica a Application/Infrastructure.
// Nota de mantenimiento: No debe contener reglas de negocio extensas; esas reglas pertenecen a Application o servicios especializados.
using Application.Abstractions.OperationalWorkflow;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Operational;

[Route("api/workshop-chief")]
[Authorize(Roles = "WorkshopChief,Admin")]
public sealed class WorkshopChiefController : OperationalControllerBase
{
    private readonly IOperationalWorkflowService _workflow;

    public WorkshopChiefController(IOperationalWorkflowService workflow)
    {
        _workflow = workflow;
    }

    [HttpGet("requests")]
    public Task<IActionResult> GetRequests(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetWorkshopChiefRequestsAsync(ct)));

    [HttpGet("requests/{requestId:int}")]
    public Task<IActionResult> GetRequest(int requestId, CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetWorkshopChiefRequestAsync(requestId, ct)));

    [HttpGet("diagnostics")]
    public Task<IActionResult> GetDiagnostics(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetWorkshopChiefDiagnosticsAsync(ct)));

    [HttpGet("diagnostics/{diagnosticId:int}")]
    public Task<IActionResult> GetDiagnostic(int diagnosticId, CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetWorkshopChiefDiagnosticAsync(diagnosticId, ct)));

    [HttpPost("diagnostics/{diagnosticId:int}/approve")]
    public Task<IActionResult> ApproveDiagnostic(int diagnosticId, ReviewMechanicDiagnosticDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.ApproveMechanicDiagnosticAsync(CurrentPersonId(), diagnosticId, dto, ct)));

    [HttpPost("diagnostics/{diagnosticId:int}/reject")]
    public Task<IActionResult> RejectDiagnostic(int diagnosticId, ReviewMechanicDiagnosticDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.RejectMechanicDiagnosticAsync(CurrentPersonId(), diagnosticId, dto, ct)));

    [HttpPost("requests/{requestId:int}/approve")]
    public Task<IActionResult> Approve(int requestId, WorkshopChiefReviewRequestDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.ApproveWorkshopChiefRequestAsync(CurrentPersonId(), requestId, dto, ct)));

    [HttpPost("requests/{requestId:int}/reject")]
    public Task<IActionResult> Reject(int requestId, WorkshopChiefReviewRequestDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.RejectWorkshopChiefRequestAsync(CurrentPersonId(), requestId, dto, ct)));
}
