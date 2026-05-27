using Domain.ValueObjects;

namespace Domain.ValueObjects.Gender;

public readonly record struct GenderName
{
    public GenderName(string value) => Value = ValueObjectValidation.Required(value, nameof(GenderName), 50);
    public string Value { get; }
}
