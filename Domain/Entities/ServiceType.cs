using Domain.Common;

namespace Domain.Entities;

public class ServiceType : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<ServiceOrderService> ServiceOrderServices { get; set; } = new List<ServiceOrderService>();
}
