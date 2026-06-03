// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de ServiceOrderEstimatedDeliveryDate, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
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
