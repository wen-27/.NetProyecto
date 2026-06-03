using Domain.ValueObjects;

namespace Domain.ValueObjects.Invoice;

// Value Object que encapsula y valida un valor especifico de InvoiceServiceOrderId.
public readonly record struct InvoiceServiceOrderId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public InvoiceServiceOrderId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(InvoiceServiceOrderId));
    }
    public int Value { get; }
}
