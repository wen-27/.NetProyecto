using Domain.ValueObjects;

namespace Domain.ValueObjects.Invoice;

public readonly record struct InvoiceStatusId
{
    public InvoiceStatusId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(InvoiceStatusId));
    }

    public int Value { get; }
}
