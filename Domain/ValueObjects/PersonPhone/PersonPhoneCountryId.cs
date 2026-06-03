using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonPhone;

// Value Object que encapsula y valida un valor especifico de PersonPhoneCountryId.
public readonly record struct PersonPhoneCountryId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonPhoneCountryId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(PersonPhoneCountryId));
    }

    public int Value { get; }
}
