// Responsabilidad: Entidad de dominio PartPurchaseDetail; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class PartPurchaseDetail : BaseEntity
{
    public int PartPurchaseId { get; set; }
    public int PartId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public PartPurchase PartPurchase { get; set; } = null!;
    public Part Part { get; set; } = null!;
}
