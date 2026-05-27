using Domain.ValueObjects;

namespace Domain.ValueObjects.User;

public readonly record struct UserPersonId
{
    public UserPersonId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(UserPersonId));
    }
    public int Value { get; }
}
