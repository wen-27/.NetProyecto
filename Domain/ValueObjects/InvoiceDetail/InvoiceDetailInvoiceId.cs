using Domain.ValueObjects;

namespace Domain.ValueObjects.InvoiceDetail;

// Value Object que encapsula y valida un valor especifico de InvoiceDetailInvoiceId.
public readonly record struct InvoiceDetailInvoiceId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public InvoiceDetailInvoiceId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(InvoiceDetailInvoiceId));
    }
    public int Value { get; }
}
