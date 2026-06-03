// Responsabilidad: Entidad de dominio ServiceOrder; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class ServiceOrder : BaseEntity
{
    public int VehicleId { get; set; }
    public int OrderStatusId { get; set; }

    public DateTime EntryDate { get; set; } = DateTime.UtcNow;
    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public decimal EstimatedTotal { get; set; }
    public string? GeneralDescription { get; set; }
    public string? WorkPerformed { get; set; }
    public string? CancellationReason { get; set; }
    public DateTime? CancellationDate { get; set; }

    public Vehicle Vehicle { get; set; } = null!;
    public OrderStatus OrderStatus { get; set; } = null!;

    public ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();
    public ICollection<OrderStatusHistory> StatusHistory { get; set; } = new List<OrderStatusHistory>();
    public Invoice? Invoice { get; set; }
    public ICollection<AdditionalServiceRequest> AdditionalServiceRequests { get; set; } = new List<AdditionalServiceRequest>();
}
