using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonPhone;

public readonly record struct PersonPhoneNumber
{
    public PersonPhoneNumber(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PersonPhoneNumber), 30);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
