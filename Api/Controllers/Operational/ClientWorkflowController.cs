using Application.Abstractions.OperationalWorkflow;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Operational;

[Route("api/client")]
[Authorize(Roles = "Client,Admin")]
public sealed class ClientWorkflowController : OperationalControllerBase
{
    private readonly IOperationalWorkflowService _workflow;

    public ClientWorkflowController(IOperationalWorkflowService workflow)
    {
        _workflow = workflow;
    }

    [HttpGet("orders")]
    public Task<IActionResult> GetOrders(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetClientOrdersAsync(CurrentPersonId(), ct)));

    [HttpGet("orders/{orderId:int}")]
    public Task<IActionResult> GetOrder(int orderId, CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetClientOrderAsync(CurrentPersonId(), orderId, ct)));

    [HttpPost("orders/{orderId:int}/approve")]
    public Task<IActionResult> ApproveOrder(int orderId, ClientReviewAdditionalRequestDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.ApproveClientOrderAsync(CurrentPersonId(), orderId, dto, ct)));

    [HttpPost("orders/{orderId:int}/reject")]
    public Task<IActionResult> RejectOrder(int orderId, ClientReviewAdditionalRequestDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.RejectClientOrderAsync(CurrentPersonId(), orderId, dto, ct)));

    [HttpGet("approvals")]
    public Task<IActionResult> GetApprovals(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetClientApprovalsAsync(CurrentPersonId(), ct)));

    [HttpPost("approvals/{requestId:int}/approve")]
    public Task<IActionResult> Approve(int requestId, ClientReviewAdditionalRequestDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.ApproveClientRequestAsync(CurrentPersonId(), requestId, dto, ct)));

    [HttpPost("approvals/{requestId:int}/reject")]
    public Task<IActionResult> Reject(int requestId, ClientReviewAdditionalRequestDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.RejectClientRequestAsync(CurrentPersonId(), requestId, dto, ct)));

    [HttpGet("messages")]
    public Task<IActionResult> GetMessages(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetClientMessagesAsync(CurrentPersonId(), ct)));

    [HttpGet("payments")]
    public Task<IActionResult> GetPayments(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetClientPaymentsAsync(CurrentPersonId(), ct)));

    [HttpGet("invoices")]
    public Task<IActionResult> GetInvoices(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetClientInvoicesAsync(CurrentPersonId(), ct)));

    [HttpPost("payments")]
    public Task<IActionResult> CreatePayment(CreateClientPaymentDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Created("/api/client/payments", await _workflow.CreateClientPaymentAsync(CurrentPersonId(), dto, ct)));
}
