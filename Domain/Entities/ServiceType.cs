using Domain.Common;

namespace Domain.Entities;

public class ServiceType : BaseEntity

{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }

    public ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
}

