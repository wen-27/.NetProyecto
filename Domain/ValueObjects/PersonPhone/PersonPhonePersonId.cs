using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonPhone;

// Value Object que encapsula y valida un valor especifico de PersonPhonePersonId.
public readonly record struct PersonPhonePersonId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonPhonePersonId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(PersonPhonePersonId));
    }
    public int Value { get; }
}
