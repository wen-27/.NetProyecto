// Responsabilidad: Entidad de dominio InvoiceDetail; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class InvoiceDetail : BaseEntity
{
    public int InvoiceId { get; set; }
    public string Concept { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public Invoice Invoice { get; set; } = null!;
}
