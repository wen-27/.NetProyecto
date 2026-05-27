using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonPhone;

public readonly record struct PersonPhoneCodeId
{
    public PersonPhoneCodeId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(PersonPhoneCodeId));
    }
    public int Value { get; }
}
