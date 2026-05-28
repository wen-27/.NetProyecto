using Application.Abstractions.OperationalWorkflow;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Operational;

[Route("api/reception")]
[Authorize(Roles = "Receptionist,Admin")]
public sealed class ReceptionController : OperationalControllerBase
{
    private readonly IOperationalWorkflowService _workflow;

    public ReceptionController(IOperationalWorkflowService workflow)
    {
        _workflow = workflow;
    }

    [HttpGet("invoices")]
    public Task<IActionResult> GetInvoices(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetReceptionInvoicesAsync(ct)));

    [HttpGet("payments/pending-verification")]
    public Task<IActionResult> GetPendingPayments(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetPendingPaymentsAsync(ct)));

    [HttpGet("payments/{paymentId:int}")]
    public Task<IActionResult> GetPayment(int paymentId, CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetPaymentAsync(paymentId, ct)));

    [HttpPost("payments/{paymentId:int}/approve")]
    public Task<IActionResult> ApprovePayment(int paymentId, ReviewPaymentDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.ApprovePaymentAsync(CurrentPersonId(), paymentId, dto, ct)));

    [HttpPost("payments/{paymentId:int}/reject")]
    public Task<IActionResult> RejectPayment(int paymentId, ReviewPaymentDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.RejectPaymentAsync(CurrentPersonId(), paymentId, dto, ct)));

    [HttpPost("orders/{orderId:int}/confirm-delivery-date")]
    public Task<IActionResult> ConfirmDeliveryDate(int orderId, ConfirmDeliveryDateDto dto, CancellationToken ct) =>
        ExecuteAsync(async () =>
        {
            await _workflow.ConfirmDeliveryDateAsync(CurrentPersonId(), orderId, dto, ct);
            return NoContent();
        });
}
