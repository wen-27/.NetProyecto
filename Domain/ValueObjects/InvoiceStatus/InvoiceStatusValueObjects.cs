using Domain.ValueObjects;

namespace Domain.ValueObjects.InvoiceStatus;

public readonly record struct InvoiceStatusName
{
    public InvoiceStatusName(string value) => Value = ValueObjectValidation.Required(value, nameof(InvoiceStatusName), 50);
    public string Value { get; }
}
