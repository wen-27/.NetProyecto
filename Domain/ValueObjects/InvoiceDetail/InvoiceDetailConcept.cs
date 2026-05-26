using Domain.ValueObjects;

namespace Domain.ValueObjects.InvoiceDetail;

public readonly record struct InvoiceDetailConcept
{
    public InvoiceDetailConcept(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(InvoiceDetailConcept), 150);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
