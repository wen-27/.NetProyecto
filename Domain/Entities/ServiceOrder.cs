using Domain.Common;

namespace Domain.Entities;

public class ServiceOrder : BaseEntity
{
    public int VehicleId { get; set; }
    public int ServiceTypeId { get; set; }
    public int MechanicId { get; set; }
    public int OrderStatusId { get; set; }

    public DateTime EntryDate { get; set; } = DateTime.UtcNow;
    public DateTime? EstimatedDeliveryDate { get; set; }
    public string? WorkPerformed { get; set; }

    public Vehicle Vehicle { get; set; } = null!;
    public ServiceType ServiceType { get; set; } = null!;
    public OrderStatus OrderStatus { get; set; } = null!;
    public User Mechanic { get; set; } = null!;

    public ICollection<OrderPartDetail> PartDetails { get; set; } = new List<OrderPartDetail>();
    public Invoice? Invoice { get; set; }
}
