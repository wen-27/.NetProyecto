// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de OrderStatusHistoryValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.OrderStatusHistory;

public readonly record struct OrderStatusHistoryServiceOrderId
{
    public OrderStatusHistoryServiceOrderId(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderStatusHistoryServiceOrderId));
    public int Value { get; }
}

public readonly record struct OrderStatusHistoryPreviousOrderStatusId
{
    public OrderStatusHistoryPreviousOrderStatusId(int? value)
    {
        if (value.HasValue && value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "El estado anterior debe ser mayor que cero.");
        }

        Value = value;
    }

    public int? Value { get; }
}

public readonly record struct OrderStatusHistoryNewOrderStatusId
{
    public OrderStatusHistoryNewOrderStatusId(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderStatusHistoryNewOrderStatusId));
    public int Value { get; }
}

public readonly record struct OrderStatusHistoryUserId
{
    public OrderStatusHistoryUserId(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderStatusHistoryUserId));
    public int Value { get; }
}

public readonly record struct OrderStatusHistoryChangeDate
{
    public OrderStatusHistoryChangeDate(DateTime value) => Value = value == default ? throw new ArgumentException("La fecha de cambio es obligatoria.", nameof(value)) : value;
    public DateTime Value { get; }
}

public readonly record struct OrderStatusHistoryObservation
{
    public OrderStatusHistoryObservation(string? value) => Value = ValueObjectValidation.Optional(value, nameof(OrderStatusHistoryObservation), 1000);
    public string? Value { get; }
}
