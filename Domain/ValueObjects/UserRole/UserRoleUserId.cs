using Domain.ValueObjects;

namespace Domain.ValueObjects.UserRole;

public readonly record struct UserRoleUserId
{
    public UserRoleUserId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(UserRoleUserId));
    }
    public int Value { get; }
}
