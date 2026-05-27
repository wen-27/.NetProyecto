using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonPhone;

public readonly record struct PersonPhoneCountryId
{
    public PersonPhoneCountryId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(PersonPhoneCountryId));
    }

    public int Value { get; }
}
