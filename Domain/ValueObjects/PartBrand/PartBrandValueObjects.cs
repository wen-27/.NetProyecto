using Domain.ValueObjects;

namespace Domain.ValueObjects.PartBrand;

public readonly record struct PartBrandName
{
    public PartBrandName(string value) => Value = ValueObjectValidation.Required(value, nameof(PartBrandName), 80);
    public string Value { get; }
}
