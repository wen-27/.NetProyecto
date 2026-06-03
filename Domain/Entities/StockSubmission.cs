using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

// Entidad de dominio que representa StockSubmission dentro del modelo principal del taller.
public class StockSubmission : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int WarehouseChiefPersonId { get; set; }
    public int? InventoryManagerPersonId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ReferenceCode { get; set; } = string.Empty;
    public int SupplierId { get; set; }
    public decimal SupplierPrice { get; set; }
    public decimal ProfitPercentage { get; set; }
    public decimal SalePrice { get; set; }
    public int Quantity { get; set; }
    public int MinimumStock { get; set; }
    public int? PartCategoryId { get; set; }
    public int? PartBrandId { get; set; }
    public string? CategoryName { get; set; }
    public string? BrandName { get; set; }
    public string? Description { get; set; }
    public string? WarehouseComment { get; set; }
    public string? InventoryManagerComment { get; set; }
    public StockSubmissionStatus Status { get; set; } = StockSubmissionStatus.Draft;
    public DateTime? ReviewedAt { get; set; }
    public DateTime? AddedToInventoryAt { get; set; }

    public Person WarehouseChiefPerson { get; set; } = null!;
    public Person? InventoryManagerPerson { get; set; }
    public Supplier Supplier { get; set; } = null!;
    public PartCategory? PartCategory { get; set; }
    public PartBrand? PartBrand { get; set; }
}
