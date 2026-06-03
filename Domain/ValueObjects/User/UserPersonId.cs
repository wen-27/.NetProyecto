using Domain.ValueObjects;

namespace Domain.ValueObjects.User;

// Value Object que encapsula y valida un valor especifico de UserPersonId.
public readonly record struct UserPersonId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public UserPersonId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(UserPersonId));
    }
    public int Value { get; }
}
