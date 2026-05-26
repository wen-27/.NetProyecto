using Domain.ValueObjects;

namespace Domain.ValueObjects.OrderPartDetail;

public readonly record struct OrderPartDetailQuantity
{
    public OrderPartDetailQuantity(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(OrderPartDetailQuantity));
    }
    public int Value { get; }
}
