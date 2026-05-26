using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

public readonly record struct PartMinimumStock
{
    public PartMinimumStock(int value)
    {
        Value = ValueObjectValidation.NonNegative(value, nameof(PartMinimumStock));
    }
    public int Value { get; }
}
