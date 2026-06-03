// Responsabilidad: Entidad de dominio VehicleEntryInventory; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class VehicleEntryInventory : BaseEntity
{
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
