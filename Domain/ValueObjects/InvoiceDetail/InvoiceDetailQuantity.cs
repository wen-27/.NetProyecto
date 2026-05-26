using Domain.ValueObjects;

namespace Domain.ValueObjects.InvoiceDetail;

public readonly record struct InvoiceDetailQuantity
{
    public InvoiceDetailQuantity(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(InvoiceDetailQuantity));
    }
    public int Value { get; }
}
