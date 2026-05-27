using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonPhone;

public readonly record struct PersonPhonePersonId
{
    public PersonPhonePersonId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(PersonPhonePersonId));
    }
    public int Value { get; }
}
