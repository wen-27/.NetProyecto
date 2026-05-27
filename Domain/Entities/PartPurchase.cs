using Domain.Common;

namespace Domain.Entities;

public class PartPurchase : BaseEntity
{
    public int SupplierId { get; set; }
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }

    public Supplier Supplier { get; set; } = null!;
    public ICollection<PartPurchaseDetail> Details { get; set; } = new List<PartPurchaseDetail>();
}
