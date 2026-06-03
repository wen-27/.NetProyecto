// Responsabilidad: Entidad de dominio OrderStatus; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class OrderStatus : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
    public ICollection<OrderStatusHistory> PreviousStatusHistory { get; set; } = new List<OrderStatusHistory>();
    public ICollection<OrderStatusHistory> NewStatusHistory { get; set; } = new List<OrderStatusHistory>();
}
