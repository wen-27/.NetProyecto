using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa OrderStatus dentro del modelo principal del taller.
public class OrderStatus : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string Name { get; set; } = string.Empty;

    public ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
    public ICollection<OrderStatusHistory> PreviousStatusHistory { get; set; } = new List<OrderStatusHistory>();
    public ICollection<OrderStatusHistory> NewStatusHistory { get; set; } = new List<OrderStatusHistory>();
}
