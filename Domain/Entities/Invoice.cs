// Responsabilidad: Entidad de dominio Invoice; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class Invoice : BaseEntity
{
    public int ServiceOrderId { get; set; }
    public int InvoiceStatusId { get; set; }
    public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
    public decimal LaborCost { get; set; }
    public decimal Total { get; set; }

    public ServiceOrder ServiceOrder { get; set; } = null!;
    public InvoiceStatus InvoiceStatus { get; set; } = null!;
    public ICollection<InvoiceDetail> Details { get; set; } = new List<InvoiceDetail>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
