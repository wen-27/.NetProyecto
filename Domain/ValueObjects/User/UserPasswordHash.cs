using Domain.ValueObjects;

namespace Domain.ValueObjects.User;

public readonly record struct UserPasswordHash
{
    public UserPasswordHash(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(UserPasswordHash), 255);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
