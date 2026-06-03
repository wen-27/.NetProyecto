using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa Invoice dentro del modelo principal del taller.
public class Invoice : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
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
