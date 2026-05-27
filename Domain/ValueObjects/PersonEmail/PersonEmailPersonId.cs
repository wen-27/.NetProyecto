using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonEmail;

public readonly record struct PersonEmailPersonId
{
    public PersonEmailPersonId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(PersonEmailPersonId));
    }
    public int Value { get; }
}
