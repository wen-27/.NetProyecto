using Application.Abstractions.OperationalWorkflow;
using Application.DTOs;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Operational;

[Route("api/warehouse")]
[Authorize(Roles = "WarehouseChief,Admin")]
// Controlador encargado de exponer por HTTP las operaciones relacionadas con Warehouse.
public sealed class WarehouseController : OperationalControllerBase
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    private readonly IOperationalWorkflowService _workflow;
    private readonly AppDbContext _context;

    public WarehouseController(IOperationalWorkflowService workflow, AppDbContext context)
    {
        _workflow = workflow;
        _context = context;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts(CancellationToken ct)
    {
        var parts = await _context.Parts.OrderBy(x => x.Description).Select(x => new { x.Id, x.Code, x.Description, x.Stock, x.MinimumStock, x.UnitPrice }).ToListAsync(ct);
        return Ok(parts);
    }

    [HttpPost("products")]
    public Task<IActionResult> CreateProduct(CreateStockSubmissionDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Created("/api/warehouse/stock-submissions", await _workflow.CreateStockSubmissionAsync(CurrentPersonId(), dto, ct)));

    [HttpPut("products/{id:int}")]
    public Task<IActionResult> UpdateProduct(int id, UpdateStockSubmissionDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.UpdateStockSubmissionAsync(CurrentPersonId(), id, dto, ct)));

    [HttpGet("stock-submissions")]
    public Task<IActionResult> GetSubmissions(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetWarehouseSubmissionsAsync(CurrentPersonId(), ct)));

    [HttpGet("stock-submissions/{id:int}")]
    public Task<IActionResult> GetSubmission(int id, CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetWarehouseSubmissionAsync(CurrentPersonId(), id, ct)));

    [HttpPost("stock-submissions")]
    public Task<IActionResult> CreateSubmission(CreateStockSubmissionDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Created("/api/warehouse/stock-submissions", await _workflow.CreateStockSubmissionAsync(CurrentPersonId(), dto, ct)));

    [HttpPost("stock-submissions/{id:int}/send-to-review")]
    public Task<IActionResult> SendToReview(int id, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.SendStockSubmissionToReviewAsync(CurrentPersonId(), id, ct)));
}
