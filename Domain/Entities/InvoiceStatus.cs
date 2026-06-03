using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa InvoiceStatus dentro del modelo principal del taller.
public class InvoiceStatus : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string Name { get; set; } = string.Empty;

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
