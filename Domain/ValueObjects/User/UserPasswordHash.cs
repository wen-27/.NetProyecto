using Domain.ValueObjects;

namespace Domain.ValueObjects.User;

// Value Object que encapsula y valida un valor especifico de UserPasswordHash.
public readonly record struct UserPasswordHash
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public UserPasswordHash(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(UserPasswordHash), 255);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
