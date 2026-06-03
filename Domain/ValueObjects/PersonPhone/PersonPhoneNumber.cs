using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonPhone;

// Value Object que encapsula y valida un valor especifico de PersonPhoneNumber.
public readonly record struct PersonPhoneNumber
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonPhoneNumber(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PersonPhoneNumber), 30);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
