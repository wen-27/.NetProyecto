using Domain.ValueObjects;

namespace Domain.ValueObjects.Person;

public readonly record struct PersonLastNames
{
    public PersonLastNames(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PersonLastNames), 100);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
