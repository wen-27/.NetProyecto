using Domain.ValueObjects;

namespace Domain.ValueObjects.UserRole;

// Value Object que encapsula y valida un valor especifico de UserRoleRoleId.
public readonly record struct UserRoleRoleId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public UserRoleRoleId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(UserRoleRoleId));
    }
    public int Value { get; }
}
