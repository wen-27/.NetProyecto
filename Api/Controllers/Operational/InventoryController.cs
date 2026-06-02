using Application.Abstractions.OperationalWorkflow;
using Application.DTOs;
using Domain.Entities;
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

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard(CancellationToken ct)
    {
        var parts = await _context.Parts.AsNoTracking().ToListAsync(ct);
        var pendingReview = await _context.StockSubmissions.CountAsync(x => x.Status == Domain.Enums.StockSubmissionStatus.PendingInventoryManagerReview, ct);
        var approvedReview = await _context.StockSubmissions.CountAsync(x => x.Status == Domain.Enums.StockSubmissionStatus.ApprovedByInventoryManager || x.Status == Domain.Enums.StockSubmissionStatus.AddedToInventory, ct);
        var rejectedReview = await _context.StockSubmissions.CountAsync(x => x.Status == Domain.Enums.StockSubmissionStatus.RejectedByInventoryManager, ct);

        return Ok(new
        {
            totalParts = parts.Count,
            activeParts = parts.Count(x => x.IsActive),
            inactiveParts = parts.Count(x => !x.IsActive),
            lowStockParts = parts.Count(x => x.Stock > 0 && x.Stock <= x.MinimumStock),
            outOfStockParts = parts.Count(x => x.Stock <= 0),
            pendingReview,
            approvedReview,
            rejectedReview
        });
    }

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
        var parts = await _context.Parts
            .Include(x => x.PartCategory)
            .Include(x => x.PartBrand)
            .OrderBy(x => x.Description)
            .Select(x => new
            {
                x.Id,
                x.PartCategoryId,
                Category = x.PartCategory.Name,
                x.PartBrandId,
                Brand = x.PartBrand == null ? null : x.PartBrand.Name,
                x.Code,
                x.Description,
                x.Stock,
                x.MinimumStock,
                x.UnitPrice,
                x.IsActive
            })
            .ToListAsync(ct);
        return Ok(parts);
    }

    [HttpPost("products")]
    public async Task<IActionResult> CreateProduct(InventoryPartRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Code) || string.IsNullOrWhiteSpace(request.Description))
        {
            return BadRequest(new { message = "El código y la descripción son obligatorios." });
        }

        if (request.MinimumStock < 0 || request.UnitPrice < 0)
        {
            return BadRequest(new { message = "El stock mínimo y el precio no pueden ser negativos." });
        }

        if (!await _context.PartCategories.AnyAsync(x => x.Id == request.PartCategoryId, ct))
        {
            return BadRequest(new { message = "La categoría no existe." });
        }

        if (request.PartBrandId.HasValue && !await _context.PartBrands.AnyAsync(x => x.Id == request.PartBrandId, ct))
        {
            return BadRequest(new { message = "La marca no existe." });
        }

        var part = new Part
        {
            PartCategoryId = request.PartCategoryId,
            PartBrandId = request.PartBrandId,
            Code = request.Code.Trim(),
            Description = request.Description.Trim(),
            Stock = 0,
            MinimumStock = request.MinimumStock,
            UnitPrice = request.UnitPrice,
            IsActive = request.IsActive
        };

        await _context.Parts.AddAsync(part, ct);
        await _context.SaveChangesAsync(ct);
        return Created($"/api/inventory/products/{part.Id}", new { part.Id });
    }

    [HttpPut("products/{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, InventoryPartRequest request, CancellationToken ct)
    {
        var part = await _context.Parts.AsTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        if (part is null)
        {
            return NotFound(new { message = "El repuesto no existe." });
        }

        if (string.IsNullOrWhiteSpace(request.Code) || string.IsNullOrWhiteSpace(request.Description))
        {
            return BadRequest(new { message = "El código y la descripción son obligatorios." });
        }

        if (request.MinimumStock < 0 || request.UnitPrice < 0)
        {
            return BadRequest(new { message = "El stock mínimo y el precio no pueden ser negativos." });
        }

        part.PartCategoryId = request.PartCategoryId;
        part.PartBrandId = request.PartBrandId;
        part.Code = request.Code.Trim();
        part.Description = request.Description.Trim();
        part.MinimumStock = request.MinimumStock;
        part.UnitPrice = request.UnitPrice;
        part.IsActive = request.IsActive;
        part.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpPatch("products/{id:int}/activate")]
    public Task<IActionResult> ActivateProduct(int id, CancellationToken ct) => SetProductActiveState(id, true, ct);

    [HttpPatch("products/{id:int}/deactivate")]
    public Task<IActionResult> DeactivateProduct(int id, CancellationToken ct) => SetProductActiveState(id, false, ct);

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories(CancellationToken ct) =>
        Ok(await _context.PartCategories.OrderBy(x => x.Name).Select(x => new { x.Id, x.Name, x.IsActive }).ToListAsync(ct));

    [HttpPost("categories")]
    public Task<IActionResult> CreateCategory(CatalogNameRequest request, CancellationToken ct) => CreateCatalogItemAsync("category", request, ct);

    [HttpPut("categories/{id:int}")]
    public Task<IActionResult> UpdateCategory(int id, CatalogNameRequest request, CancellationToken ct) => UpdateCatalogItemAsync("category", id, request, ct);

    [HttpGet("brands")]
    public async Task<IActionResult> GetBrands(CancellationToken ct) =>
        Ok(await _context.PartBrands.OrderBy(x => x.Name).Select(x => new { x.Id, x.Name, x.IsActive }).ToListAsync(ct));

    [HttpPost("brands")]
    public Task<IActionResult> CreateBrand(CatalogNameRequest request, CancellationToken ct) => CreateCatalogItemAsync("brand", request, ct);

    [HttpPut("brands/{id:int}")]
    public Task<IActionResult> UpdateBrand(int id, CatalogNameRequest request, CancellationToken ct) => UpdateCatalogItemAsync("brand", id, request, ct);

    [HttpGet("suppliers")]
    public async Task<IActionResult> GetSuppliers(CancellationToken ct) =>
        Ok(await _context.Suppliers.OrderBy(x => x.Name).Select(x => new { x.Id, x.Name, x.TaxId, x.Phone, x.Email, Status = x.Status }).ToListAsync(ct));

    [HttpPost("suppliers")]
    public async Task<IActionResult> CreateSupplier(InventorySupplierRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { message = "El proveedor es obligatorio." });
        }

        var supplier = new Supplier
        {
            Name = request.Name.Trim(),
            TaxId = request.TaxId?.Trim(),
            Phone = request.Phone?.Trim(),
            Email = request.Email?.Trim(),
            Status = request.Status
        };

        await _context.Suppliers.AddAsync(supplier, ct);
        await _context.SaveChangesAsync(ct);
        return Created($"/api/inventory/suppliers/{supplier.Id}", new { supplier.Id });
    }

    [HttpPut("suppliers/{id:int}")]
    public async Task<IActionResult> UpdateSupplier(int id, InventorySupplierRequest request, CancellationToken ct)
    {
        var supplier = await _context.Suppliers.AsTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        if (supplier is null)
        {
            return NotFound(new { message = "El proveedor no existe." });
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { message = "El proveedor es obligatorio." });
        }

        supplier.Name = request.Name.Trim();
        supplier.TaxId = request.TaxId?.Trim();
        supplier.Phone = request.Phone?.Trim();
        supplier.Email = request.Email?.Trim();
        supplier.Status = request.Status;
        supplier.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHistory(CancellationToken ct)
    {
        var history = await _context.InventoryHistory
            .Include(x => x.Part)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new { x.Id, x.PartId, PartCode = x.Part.Code, PartName = x.Part.Description, x.QuantityChange, x.ResultingStock, x.UnitPrice, x.Action, x.Comment, x.CreatedAt })
            .ToListAsync(ct);
        return Ok(history);
    }

    private async Task<IActionResult> SetProductActiveState(int id, bool isActive, CancellationToken ct)
    {
        var part = await _context.Parts.AsTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        if (part is null)
        {
            return NotFound(new { message = "El repuesto no existe." });
        }

        part.IsActive = isActive;
        part.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(ct);
        return NoContent();
    }

    private async Task<IActionResult> CreateCatalogItemAsync(string catalog, CatalogNameRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { message = "El nombre es obligatorio." });
        }

        if (catalog == "category")
        {
            var category = new PartCategory { Name = request.Name.Trim() };
            await _context.PartCategories.AddAsync(category, ct);
            await _context.SaveChangesAsync(ct);
            return Created($"/api/inventory/categories/{category.Id}", new { category.Id });
        }

        var brand = new PartBrand { Name = request.Name.Trim() };
        await _context.PartBrands.AddAsync(brand, ct);
        await _context.SaveChangesAsync(ct);
        return Created($"/api/inventory/brands/{brand.Id}", new { brand.Id });
    }

    private async Task<IActionResult> UpdateCatalogItemAsync(string catalog, int id, CatalogNameRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { message = "El nombre es obligatorio." });
        }

        if (catalog == "category")
        {
            var category = await _context.PartCategories.AsTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
            if (category is null) return NotFound(new { message = "La categoría no existe." });
            category.Name = request.Name.Trim();
            category.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            var brand = await _context.PartBrands.AsTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
            if (brand is null) return NotFound(new { message = "La marca no existe." });
            brand.Name = request.Name.Trim();
            brand.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync(ct);
        return NoContent();
    }
}

public sealed record InventoryPartRequest(
    int PartCategoryId,
    int? PartBrandId,
    string Code,
    string Description,
    int MinimumStock,
    decimal UnitPrice,
    bool IsActive);

public sealed record CatalogNameRequest(string Name);

public sealed record InventorySupplierRequest(string Name, string? TaxId, string? Phone, string? Email, bool Status);
