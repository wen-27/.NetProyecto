using Domain.ValueObjects;

namespace Domain.ValueObjects.OrderStatusHistory;

// Value Object que encapsula y valida un valor especifico de OrderStatusHistoryServiceOrderId.
public readonly record struct OrderStatusHistoryServiceOrderId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderStatusHistoryServiceOrderId(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderStatusHistoryServiceOrderId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de OrderStatusHistoryPreviousOrderStatusId.
public readonly record struct OrderStatusHistoryPreviousOrderStatusId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
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

// Value Object que encapsula y valida un valor especifico de OrderStatusHistoryNewOrderStatusId.
public readonly record struct OrderStatusHistoryNewOrderStatusId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderStatusHistoryNewOrderStatusId(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderStatusHistoryNewOrderStatusId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de OrderStatusHistoryUserId.
public readonly record struct OrderStatusHistoryUserId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderStatusHistoryUserId(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderStatusHistoryUserId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de OrderStatusHistoryChangeDate.
public readonly record struct OrderStatusHistoryChangeDate
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderStatusHistoryChangeDate(DateTime value) => Value = value == default ? throw new ArgumentException("La fecha de cambio es obligatoria.", nameof(value)) : value;
    public DateTime Value { get; }
}

// Value Object que encapsula y valida un valor especifico de OrderStatusHistoryObservation.
public readonly record struct OrderStatusHistoryObservation
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderStatusHistoryObservation(string? value) => Value = ValueObjectValidation.Optional(value, nameof(OrderStatusHistoryObservation), 1000);
    public string? Value { get; }
}
