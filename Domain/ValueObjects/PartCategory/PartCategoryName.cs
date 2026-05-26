using Domain.ValueObjects;

namespace Domain.ValueObjects.PartCategory;

public readonly record struct PartCategoryName
{
    public PartCategoryName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PartCategoryName), 80);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
