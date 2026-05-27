using Domain.Common;

namespace Domain.Entities;

public class InvoiceStatus : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
