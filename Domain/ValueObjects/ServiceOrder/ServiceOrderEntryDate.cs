namespace Domain.ValueObjects.ServiceOrder;

public readonly record struct ServiceOrderEntryDate
{
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
