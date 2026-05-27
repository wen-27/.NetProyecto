using Domain.ValueObjects;

namespace Domain.ValueObjects.UserRole;

public readonly record struct UserRoleRoleId
{
    public UserRoleRoleId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(UserRoleRoleId));
    }
    public int Value { get; }
}
