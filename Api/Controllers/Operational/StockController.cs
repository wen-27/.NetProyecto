// Responsabilidad: Controlador HTTP que expone endpoints REST relacionados con Stock. Coordina validacion de entrada, autorizacion y delega la logica a Application/Infrastructure.
// Nota de mantenimiento: No debe contener reglas de negocio extensas; esas reglas pertenecen a Application o servicios especializados.
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Operational;

[ApiController]
[Route("api/stock")]
[Authorize(Roles = "WarehouseChief,Admin")]
public sealed class StockController : ControllerBase
{
    private readonly AppDbContext _context;

    public StockController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard(CancellationToken ct)
    {
        var parts = await PartsQuery().ToListAsync(ct);
        var movements = await MovementsBaseQuery().Take(10).ToListAsync(ct);
        return Ok(new
        {
            totalParts = parts.Count,
            availableParts = parts.Count(x => x.Stock > x.MinimumStock),
            lowStockParts = parts.Count(x => x.Stock > 0 && x.Stock <= x.MinimumStock),
            outOfStockParts = parts.Count(x => x.Stock <= 0),
            recentMovements = movements.Select(ToStockMovementDto)
        });
    }

    [HttpGet("parts")]
    public async Task<IActionResult> GetParts([FromQuery] string? search = null, [FromQuery] string? stockStatus = null, CancellationToken ct = default)
    {
        var query = PartsQuery();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(x =>
                x.Code.ToLower().Contains(term) ||
                x.Description.ToLower().Contains(term) ||
                x.PartCategory.Name.ToLower().Contains(term) ||
                (x.PartBrand != null && x.PartBrand.Name.ToLower().Contains(term)));
        }

        query = NormalizeStockStatus(stockStatus) switch
        {
            "available" => query.Where(x => x.Stock > x.MinimumStock),
            "low" => query.Where(x => x.Stock <= x.MinimumStock),
            "out" => query.Where(x => x.Stock <= 0),
            _ => query
        };

        var parts = await query
            .OrderBy(x => x.Description)
            .ToListAsync(ct);

        return Ok(parts.Select(ToStockPartDto));
    }

    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStock(CancellationToken ct)
    {
        var parts = await PartsQuery()
            .Where(x => x.Stock <= x.MinimumStock)
            .OrderBy(x => x.Stock)
            .ToListAsync(ct);
        return Ok(parts.Select(ToStockPartDto));
    }

    [HttpGet("out-of-stock")]
    public async Task<IActionResult> GetOutOfStock(CancellationToken ct)
    {
        var parts = await PartsQuery()
            .Where(x => x.Stock <= 0)
            .OrderBy(x => x.Description)
            .ToListAsync(ct);
        return Ok(parts.Select(ToStockPartDto));
    }

    [HttpGet("movements")]
    public async Task<IActionResult> GetMovements(CancellationToken ct)
    {
        var movements = await MovementsBaseQuery().ToListAsync(ct);
        return Ok(movements.Select(ToStockMovementDto));
    }

    [HttpGet("parts/{id:int}/movements")]
    public async Task<IActionResult> GetPartMovements(int id, CancellationToken ct)
    {
        var movements = await MovementsBaseQuery()
            .Where(x => x.PartId == id)
            .ToListAsync(ct);

        return Ok(movements.Select(ToStockMovementDto));
    }

    [HttpPost("movements/in")]
    public Task<IActionResult> RegisterStockIn(StockMovementRequest request, CancellationToken ct) =>
        RegisterMovementAsync(request, isEntry: true, ct);

    [HttpPost("movements/out")]
    public Task<IActionResult> RegisterStockOut(StockMovementRequest request, CancellationToken ct) =>
        RegisterMovementAsync(request, isEntry: false, ct);

    private IQueryable<Part> PartsQuery() =>
        _context.Parts
            .AsNoTracking()
            .Include(x => x.PartCategory)
            .Include(x => x.PartBrand);

    private IQueryable<InventoryHistory> MovementsBaseQuery() =>
        _context.InventoryHistory
            .AsNoTracking()
            .Include(x => x.Part)
            .OrderByDescending(x => x.CreatedAt);

    private async Task<IActionResult> RegisterMovementAsync(StockMovementRequest request, bool isEntry, CancellationToken ct)
    {
        if (request.PartId <= 0)
        {
            return BadRequest(new { message = "El repuesto es obligatorio." });
        }

        if (request.Quantity <= 0)
        {
            return BadRequest(new { message = "La cantidad debe ser mayor a cero." });
        }

        var part = await _context.Parts.AsTracking().FirstOrDefaultAsync(x => x.Id == request.PartId, ct);
        if (part is null)
        {
            return NotFound(new { message = "El repuesto no existe." });
        }

        var quantityChange = isEntry ? request.Quantity : -request.Quantity;
        if (!isEntry && part.Stock < request.Quantity)
        {
            return BadRequest(new { message = "No se puede registrar una salida que deje stock negativo." });
        }

        part.Stock += quantityChange;
        part.UpdatedAt = DateTime.UtcNow;

        var movement = new InventoryHistory
        {
            PartId = part.Id,
            QuantityChange = quantityChange,
            ResultingStock = part.Stock,
            UnitPrice = part.UnitPrice,
            Action = isEntry ? "Entrada de stock" : "Salida de stock",
            Comment = request.Comment?.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        await _context.InventoryHistory.AddAsync(movement, ct);
        await _context.SaveChangesAsync(ct);

        return Created($"/api/stock/movements/{movement.Id}", new
        {
            movement.Id,
            movement.PartId,
            part.Code,
            PartName = part.Description,
            movement.QuantityChange,
            movement.ResultingStock,
            movement.UnitPrice,
            movement.Action,
            movement.Comment,
            movement.CreatedAt
        });
    }

    private static string? NormalizeStockStatus(string? stockStatus) =>
        stockStatus?.Trim().ToLowerInvariant() switch
        {
            "disponible" or "available" => "available",
            "bajo" or "bajo stock" or "low" or "low-stock" => "low",
            "agotado" or "out" or "out-of-stock" => "out",
            _ => null
        };

    private static StockPartDto ToStockPartDto(Part part)
    {
        var status = part.Stock <= 0
            ? "Agotado"
            : part.Stock <= part.MinimumStock
                ? "Bajo stock"
                : "Disponible";

        return new StockPartDto(
            part.Id,
            part.Code,
            part.Description,
            part.PartCategory.Name,
            part.PartBrand?.Name,
            part.Stock,
            part.MinimumStock,
            part.UnitPrice,
            part.IsActive,
            status);
    }

    private static StockMovementDto ToStockMovementDto(InventoryHistory movement) =>
        new(
            movement.Id,
            movement.PartId,
            movement.Part.Code,
            movement.Part.Description,
            movement.QuantityChange,
            movement.ResultingStock,
            movement.UnitPrice,
            movement.Action,
            movement.Comment,
            movement.CreatedAt);
}

public sealed record StockMovementRequest(int PartId, int Quantity, string? Comment);

public sealed record StockPartDto(
    int Id,
    string Code,
    string Description,
    string Category,
    string? Brand,
    int Stock,
    int MinimumStock,
    decimal UnitPrice,
    bool IsActive,
    string StockStatus);

public sealed record StockMovementDto(
    int Id,
    int PartId,
    string PartCode,
    string PartName,
    int QuantityChange,
    int ResultingStock,
    decimal UnitPrice,
    string Action,
    string? Comment,
    DateTime CreatedAt);
