using Domain.Common;

namespace Domain.Entities;

public class Part : BaseEntity
{
    public int PartCategoryId { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int MinimumStock { get; set; }
    public decimal UnitPrice { get; set; }

    public PartCategory PartCategory { get; set; } = null!;
    public ICollection<OrderPartDetail> OrderDetails { get; set; } = new List<OrderPartDetail>();
}
