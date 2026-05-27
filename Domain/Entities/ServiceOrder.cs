using Domain.Common;

namespace Domain.Entities;

public class ServiceOrder : BaseEntity
{
    public int VehicleId { get; set; }
    public int OrderStatusId { get; set; }

    public DateTime EntryDate { get; set; } = DateTime.UtcNow;
    public DateTime? EstimatedDeliveryDate { get; set; }
    public string? GeneralDescription { get; set; }
    public string? WorkPerformed { get; set; }
    public string? CancellationReason { get; set; }
    public DateTime? CancellationDate { get; set; }

    public Vehicle Vehicle { get; set; } = null!;
    public OrderStatus OrderStatus { get; set; } = null!;

    public ICollection<ServiceOrderService> Services { get; set; } = new List<ServiceOrderService>();
    public ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();
    public ICollection<OrderPartDetail> PartDetails { get; set; } = new List<OrderPartDetail>();
    public ICollection<OrderStatusHistory> StatusHistory { get; set; } = new List<OrderStatusHistory>();
    public Invoice? Invoice { get; set; }
}
