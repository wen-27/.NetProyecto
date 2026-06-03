using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa PartPurchaseDetail dentro del modelo principal del taller.
public class PartPurchaseDetail : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int PartPurchaseId { get; set; }
    public int PartId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public PartPurchase PartPurchase { get; set; } = null!;
    public Part Part { get; set; } = null!;
}
