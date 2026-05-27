using Domain.Common;

namespace Domain.Entities;

public class OrderService : BaseEntity
{
    public int ServiceOrderId { get; set; }
    public int ServiceTypeId { get; set; }
    public string? Description { get; set; }
    public string? WorkPerformed { get; set; }
    public decimal LaborCost { get; set; }
    public bool? CustomerApproved { get; set; }
    public DateTime? ApprovalDate { get; set; }

    public ServiceOrder ServiceOrder { get; set; } = null!;
    public ServiceType ServiceType { get; set; } = null!;
}
