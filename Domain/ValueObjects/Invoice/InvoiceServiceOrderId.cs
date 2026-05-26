using Domain.ValueObjects;

namespace Domain.ValueObjects.Invoice;

public readonly record struct InvoiceServiceOrderId
{
    public InvoiceServiceOrderId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(InvoiceServiceOrderId));
    }
    public int Value { get; }
}
