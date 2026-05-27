using Domain.Common;

namespace Domain.Entities;

public class Payment : BaseEntity
{
    public int InvoiceId { get; set; }
    public int PaymentMethodId { get; set; }
    public int PaymentStatusId { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public string? Reference { get; set; }

    public Invoice Invoice { get; set; } = null!;
    public PaymentMethod PaymentMethod { get; set; } = null!;
    public PaymentStatus PaymentStatus { get; set; } = null!;
    public PaymentCard? PaymentCard { get; set; }
}
