using Domain.Common;

namespace Domain.Entities;

public class OrderServicePart : BaseEntity
{
    public int OrderServiceId { get; set; }
    public int PartId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal AppliedUnitPrice { get; set; }
    public bool? CustomerApproved { get; set; }
    public DateTime? ApprovalDate { get; set; }

    public OrderService OrderService { get; set; } = null!;
    public Part Part { get; set; } = null!;
}
