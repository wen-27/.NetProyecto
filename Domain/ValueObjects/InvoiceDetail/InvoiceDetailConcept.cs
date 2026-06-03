using Domain.ValueObjects;

namespace Domain.ValueObjects.InvoiceDetail;

// Value Object que encapsula y valida un valor especifico de InvoiceDetailConcept.
public readonly record struct InvoiceDetailConcept
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public InvoiceDetailConcept(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(InvoiceDetailConcept), 150);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
