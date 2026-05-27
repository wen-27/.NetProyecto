using Domain.ValueObjects;

namespace Domain.ValueObjects.StreetType;

public readonly record struct StreetTypeName
{
    public StreetTypeName(string value) => Value = ValueObjectValidation.Required(value, nameof(StreetTypeName), 50);
    public string Value { get; }
}
