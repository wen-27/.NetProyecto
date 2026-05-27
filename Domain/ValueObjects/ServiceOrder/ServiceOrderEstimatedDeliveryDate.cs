namespace Domain.ValueObjects.ServiceOrder;

public readonly record struct ServiceOrderEstimatedDeliveryDate
{
    public DateTime? Value { get; }

    public ServiceOrderEstimatedDeliveryDate(DateTime? value)
    {
        if (value == default(DateTime))
        {
            throw new ArgumentException("La fecha estimada de entrega no puede ser la fecha predeterminada.", nameof(value));
        }

        Value = value;
    }

    public bool HasValue => Value.HasValue;

    public override string ToString() => Value?.ToString("O") ?? string.Empty;
}
