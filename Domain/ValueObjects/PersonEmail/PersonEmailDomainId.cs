using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonEmail;

public readonly record struct PersonEmailDomainId
{
    public PersonEmailDomainId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(PersonEmailDomainId));
    }
    public int Value { get; }
}
