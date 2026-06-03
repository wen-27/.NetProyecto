using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa OrderStatusHistory dentro del modelo principal del taller.
public class OrderStatusHistory : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
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
