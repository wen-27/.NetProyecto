using Domain.ValueObjects;

namespace Domain.ValueObjects.UserRole;

// Value Object que encapsula y valida un valor especifico de UserRoleUserId.
public readonly record struct UserRoleUserId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public UserRoleUserId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(UserRoleUserId));
    }
    public int Value { get; }
}
