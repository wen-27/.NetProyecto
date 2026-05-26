using Domain.Common;

namespace Domain.Entities;

public class OrderPartDetail : BaseEntity
{
    public int ServiceOrderId { get; set; }
    public int PartId { get; set; }
    public int Quantity { get; set; }
    public decimal AppliedUnitPrice { get; set; }

    public ServiceOrder ServiceOrder { get; set; } = null!;
    public Part Part { get; set; } = null!;
}
