using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa InvoiceDetail dentro del modelo principal del taller.
public class InvoiceDetail : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int InvoiceId { get; set; }
    public string Concept { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public Invoice Invoice { get; set; } = null!;
}
