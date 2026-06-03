using Domain.ValueObjects;

namespace Domain.ValueObjects.Invoice;

// Value Object que encapsula y valida un valor especifico de InvoiceStatusId.
public readonly record struct InvoiceStatusId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public InvoiceStatusId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(InvoiceStatusId));
    }

    public int Value { get; }
}
