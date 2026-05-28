using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class WorkshopService : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal LaborPercentage { get; set; }
    public decimal PartsSubtotal { get; set; }
    public decimal LaborAmount { get; set; }
    public decimal FinalPrice { get; set; }
    public WorkshopServiceStatus Status { get; set; } = WorkshopServiceStatus.Active;

    public ICollection<WorkshopServicePart> Parts { get; set; } = new List<WorkshopServicePart>();
    public ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();
    public ICollection<AdditionalServiceRequest> AdditionalServiceRequests { get; set; } = new List<AdditionalServiceRequest>();
}
