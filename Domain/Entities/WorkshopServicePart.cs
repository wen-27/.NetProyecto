using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa WorkshopServicePart dentro del modelo principal del taller.
public class WorkshopServicePart : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int WorkshopServiceId { get; set; }
    public int PartId { get; set; }
    public int QuantityRequired { get; set; }
    public decimal UnitSalePrice { get; set; }
    public decimal LineTotal { get; set; }

    public WorkshopService WorkshopService { get; set; } = null!;
    public Part Part { get; set; } = null!;
}
