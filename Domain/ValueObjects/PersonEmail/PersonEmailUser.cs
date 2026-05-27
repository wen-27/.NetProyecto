using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonEmail;

public readonly record struct PersonEmailUser
{
    public PersonEmailUser(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PersonEmailUser), 100);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
