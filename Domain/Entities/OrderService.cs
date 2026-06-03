using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

// Entidad de dominio que representa OrderService dentro del modelo principal del taller.
public class OrderService : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int ServiceOrderId { get; set; }
    public int ServiceTypeId { get; set; }
    public int? WorkshopServiceId { get; set; }
    public string? Description { get; set; }
    public string? WorkPerformed { get; set; }
    public decimal LaborCost { get; set; }
    public decimal Price { get; set; }
    public OrderServiceStatus Status { get; set; } = OrderServiceStatus.Pending;
    public bool? CustomerApproved { get; set; }
    public DateTime? ApprovalDate { get; set; }

    public ServiceOrder ServiceOrder { get; set; } = null!;
    public ServiceType ServiceType { get; set; } = null!;
    public WorkshopService? WorkshopService { get; set; }
    public ICollection<OrderServicePart> Parts { get; set; } = new List<OrderServicePart>();
}
