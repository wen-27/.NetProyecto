using Domain.Common;

namespace Domain.Entities;

public class InvoiceDetail : BaseEntity

{
    public int InvoiceId { get; set; }
    public string Concept { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }

    public Invoice Invoice { get; set; } = null!;
}