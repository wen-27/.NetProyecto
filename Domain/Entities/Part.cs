using Domain.Common;

namespace Domain.Entities;

public class Part : BaseEntity
{
    public int PartCategoryId { get; set; }
    public int? PartBrandId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int MinimumStock { get; set; }
    public decimal UnitPrice { get; set; }

    public PartCategory PartCategory { get; set; } = null!;
    public PartBrand? PartBrand { get; set; }
    public ICollection<OrderPartDetail> OrderDetails { get; set; } = new List<OrderPartDetail>();
    public ICollection<PartPurchaseDetail> PurchaseDetails { get; set; } = new List<PartPurchaseDetail>();
}
