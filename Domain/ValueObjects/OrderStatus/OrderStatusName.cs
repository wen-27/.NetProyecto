using Domain.ValueObjects;

namespace Domain.ValueObjects.OrderStatus;

public readonly record struct OrderStatusName
{
    public OrderStatusName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(OrderStatusName), 50);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
