using Domain.Common;

namespace Domain.Entities;

public class OrderStatus : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
    public ICollection<OrderStatusHistory> PreviousStatusHistory { get; set; } = new List<OrderStatusHistory>();
    public ICollection<OrderStatusHistory> NewStatusHistory { get; set; } = new List<OrderStatusHistory>();
}
