using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa InventoryHistory dentro del modelo principal del taller.
public class InventoryHistory : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int PartId { get; set; }
    public int? StockSubmissionId { get; set; }
    public int QuantityChange { get; set; }
    public int ResultingStock { get; set; }
    public decimal UnitPrice { get; set; }
    public string Action { get; set; } = string.Empty;
    public string? Comment { get; set; }

    public Part Part { get; set; } = null!;
    public StockSubmission? StockSubmission { get; set; }
}
