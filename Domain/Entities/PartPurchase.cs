using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa PartPurchase dentro del modelo principal del taller.
public class PartPurchase : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int SupplierId { get; set; }
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }

    public Supplier Supplier { get; set; } = null!;
    public ICollection<PartPurchaseDetail> Details { get; set; } = new List<PartPurchaseDetail>();
}
