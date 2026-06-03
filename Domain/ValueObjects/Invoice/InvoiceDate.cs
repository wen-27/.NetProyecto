namespace Domain.ValueObjects.Invoice;

// Value Object que encapsula y valida un valor especifico de InvoiceDate.
public readonly record struct InvoiceDate
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public DateTime Value { get; }

    public InvoiceDate(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de la factura es obligatoria.", nameof(value));
        }

        if (value > DateTime.UtcNow.AddMinutes(5))
        {
            throw new ArgumentOutOfRangeException(nameof(value), "La fecha de la factura no puede estar en el futuro.");
        }

        Value = value;
    }

    public override string ToString() => Value.ToString("O");
}
