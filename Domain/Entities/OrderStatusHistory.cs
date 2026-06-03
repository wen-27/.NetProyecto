// Responsabilidad: Entidad de dominio OrderStatusHistory; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class OrderStatusHistory : BaseEntity
{
    public int ServiceOrderId { get; set; }
    public int? PreviousOrderStatusId { get; set; }
    public int NewOrderStatusId { get; set; }
    public int UserId { get; set; }
    public DateTime ChangeDate { get; set; } = DateTime.UtcNow;
    public string? Observation { get; set; }

    public ServiceOrder ServiceOrder { get; set; } = null!;
    public OrderStatus? PreviousOrderStatus { get; set; }
    public OrderStatus NewOrderStatus { get; set; } = null!;
    public User User { get; set; } = null!;
}
