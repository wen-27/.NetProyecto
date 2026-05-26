using Domain.ValueObjects;

namespace Domain.ValueObjects.OrderPartDetail;

public readonly record struct OrderPartDetailServiceOrderId
{
    public OrderPartDetailServiceOrderId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(OrderPartDetailServiceOrderId));
    }
    public int Value { get; }
}
