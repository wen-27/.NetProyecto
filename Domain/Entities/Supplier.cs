using Domain.Common;

namespace Domain.Entities;

public class Supplier : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? TaxId { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool Status { get; set; } = true;

    public ICollection<PartPurchase> PartPurchases { get; set; } = new List<PartPurchase>();
}
