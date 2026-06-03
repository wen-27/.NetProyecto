using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa VehicleEntryInventory dentro del modelo principal del taller.
public class VehicleEntryInventory : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int ServiceOrderId { get; set; }
    public bool HasScratches { get; set; }
    public string? ScratchesDescription { get; set; }
    public bool HasToolbox { get; set; }
    public string? ToolboxDescription { get; set; }
    public bool OwnershipCardDelivered { get; set; }
    public string? Observations { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    public ServiceOrder ServiceOrder { get; set; } = null!;
}
