namespace Domain.ValueObjects.ServiceOrder;

// Value Object que encapsula y valida un valor especifico de ServiceOrderEntryDate.
public readonly record struct ServiceOrderEntryDate
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public DateTime Value { get; }

    public ServiceOrderEntryDate(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de ingreso es obligatoria.", nameof(value));
        }

        if (value > DateTime.UtcNow.AddMinutes(5))
        {
            throw new ArgumentOutOfRangeException(nameof(value), "La fecha de ingreso no puede estar en el futuro.");
        }

        Value = value;
    }

    public override string ToString() => Value.ToString("O");
}
