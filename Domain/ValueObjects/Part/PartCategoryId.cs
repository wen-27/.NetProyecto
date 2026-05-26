using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

public readonly record struct PartCategoryId
{
    public PartCategoryId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(PartCategoryId));
    }
    public int Value { get; }
}
