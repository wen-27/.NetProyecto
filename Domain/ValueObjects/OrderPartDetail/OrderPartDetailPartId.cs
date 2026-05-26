using Domain.ValueObjects;

namespace Domain.ValueObjects.OrderPartDetail;

public readonly record struct OrderPartDetailPartId
{
    public OrderPartDetailPartId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(OrderPartDetailPartId));
    }
    public int Value { get; }
}
