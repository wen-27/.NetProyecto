using Domain.ValueObjects;

namespace Domain.ValueObjects.InvoiceDetail;

// Value Object que encapsula y valida un valor especifico de InvoiceDetailQuantity.
public readonly record struct InvoiceDetailQuantity
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public InvoiceDetailQuantity(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(InvoiceDetailQuantity));
    }
    public int Value { get; }
}
