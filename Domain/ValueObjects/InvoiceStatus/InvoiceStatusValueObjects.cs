using Domain.ValueObjects;

namespace Domain.ValueObjects.InvoiceStatus;

// Value Object que encapsula y valida un valor especifico de InvoiceStatusName.
public readonly record struct InvoiceStatusName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public InvoiceStatusName(string value) => Value = ValueObjectValidation.Required(value, nameof(InvoiceStatusName), 50);
    public string Value { get; }
}
