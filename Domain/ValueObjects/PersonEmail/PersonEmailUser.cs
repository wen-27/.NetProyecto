using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonEmail;

// Value Object que encapsula y valida un valor especifico de PersonEmailUser.
public readonly record struct PersonEmailUser
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonEmailUser(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PersonEmailUser), 100);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
