using Domain.Common;

namespace Domain.Entities;

public class ServiceOrderService : BaseEntity
{
    public int ServiceOrderId { get; set; }
    public int ServiceTypeId { get; set; }
    public int MechanicId { get; set; }
    public string? Description { get; set; }
    public decimal LaborCost { get; set; }

    public ServiceOrder ServiceOrder { get; set; } = null!;
    public ServiceType ServiceType { get; set; } = null!;
    public User Mechanic { get; set; } = null!;
}
