using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

public readonly record struct PartStock
{
    public PartStock(int value)
    {
        Value = ValueObjectValidation.NonNegative(value, nameof(PartStock));
    }
    public int Value { get; }
}
