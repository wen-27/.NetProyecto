using Domain.Common;

namespace Domain.Entities;

public class WorkshopServicePart : BaseEntity
{
    public int WorkshopServiceId { get; set; }
    public int PartId { get; set; }
    public int QuantityRequired { get; set; }
    public decimal UnitSalePrice { get; set; }
    public decimal LineTotal { get; set; }

    public WorkshopService WorkshopService { get; set; } = null!;
    public Part Part { get; set; } = null!;
}
