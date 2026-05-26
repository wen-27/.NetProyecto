using Domain.ValueObjects;

namespace Domain.ValueObjects.Person;

public readonly record struct PersonFirstNames
{
    public PersonFirstNames(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PersonFirstNames), 100);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
