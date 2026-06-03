// Responsabilidad: Entidad de dominio Supplier; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class Supplier : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? TaxId { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool Status { get; set; } = true;

    public ICollection<PartPurchase> PartPurchases { get; set; } = new List<PartPurchase>();
}
