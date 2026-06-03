namespace Domain.ValueObjects.ServiceOrder;

// Value Object que encapsula y valida un valor especifico de ServiceOrderEstimatedDeliveryDate.
public readonly record struct ServiceOrderEstimatedDeliveryDate
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
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
