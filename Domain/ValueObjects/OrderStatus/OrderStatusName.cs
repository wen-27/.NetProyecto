using Domain.ValueObjects;

namespace Domain.ValueObjects.OrderStatus;

// Value Object que encapsula y valida un valor especifico de OrderStatusName.
public readonly record struct OrderStatusName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderStatusName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(OrderStatusName), 50);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
