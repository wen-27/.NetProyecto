using Domain.ValueObjects;

namespace Domain.ValueObjects.InvoiceDetail;

public readonly record struct InvoiceDetailInvoiceId
{
    public InvoiceDetailInvoiceId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(InvoiceDetailInvoiceId));
    }
    public int Value { get; }
}
