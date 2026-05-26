using Domain.Common;

namespace Domain.Entities;

public class ServiceOrder : BaseEntity
{
    public int VehicleId { get; set; }
    public int ServiceTypeId { get; set; }
    public int OrderStatusId { get; set; }
    public int MechanicUserId { get; set; }

    public DateTime EntryDate { get; set; } = DateTime.UtcNow;
    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ClosingDate { get; set; }

    public string ProblemDescription { get; set; } = string.Empty;
    public string? Diagnosis { get; set; }
    public decimal LaborCost { get; set; }

    public Vehicle Vehicle { get; set; } = null!;
    public ServiceType ServiceType { get; set; } = null!;
    public OrderStatus OrderStatus { get; set; } = null!;
    public User MechanicUser { get; set; } = null!;

    public ICollection<OrderPartDetail> PartDetails { get; set; } = new List<OrderPartDetail>();
    public Invoice? Invoice { get; set; }
}