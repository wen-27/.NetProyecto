using Domain.Common;

namespace Domain.Entities;

public class ServiceType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int EstimatedDays { get; set; } = 1;

    public ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();
}
