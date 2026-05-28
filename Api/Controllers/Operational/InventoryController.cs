using Application.Abstractions.OperationalWorkflow;
using Application.DTOs;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Operational;

[Route("api/inventory")]
[Authorize(Roles = "InventoryManager,Admin")]
public sealed class InventoryController : OperationalControllerBase
{
    private readonly IOperationalWorkflowService _workflow;
    private readonly AppDbContext _context;

    public InventoryController(IOperationalWorkflowService workflow, AppDbContext context)
    {
        _workflow = workflow;
        _context = context;
    }

    [HttpGet("review-requests")]
    public Task<IActionResult> GetReviewRequests(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetInventoryReviewRequestsAsync(ct)));

    [HttpGet("review-requests/{id:int}")]
    public Task<IActionResult> GetReviewRequest(int id, CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetInventoryReviewRequestAsync(id, ct)));

    [HttpPost("review-requests/{id:int}/approve")]
    public Task<IActionResult> Approve(int id, ReviewStockSubmissionDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.ApproveStockSubmissionAsync(CurrentPersonId(), id, dto, ct)));

    [HttpPost("review-requests/{id:int}/reject")]
    public Task<IActionResult> Reject(int id, ReviewStockSubmissionDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.RejectStockSubmissionAsync(CurrentPersonId(), id, dto, ct)));

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts(CancellationToken ct)
    {
        var parts = await _context.Parts.OrderBy(x => x.Description).Select(x => new { x.Id, x.Code, x.Description, x.Stock, x.MinimumStock, x.UnitPrice }).ToListAsync(ct);
        return Ok(parts);
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHistory(CancellationToken ct)
    {
        var history = await _context.InventoryHistory
            .Include(x => x.Part)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new { x.Id, x.PartId, PartName = x.Part.Description, x.QuantityChange, x.ResultingStock, x.UnitPrice, x.Action, x.Comment, x.CreatedAt })
            .ToListAsync(ct);
        return Ok(history);
    }
}
