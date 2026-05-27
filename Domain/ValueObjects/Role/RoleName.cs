using Domain.ValueObjects;

namespace Domain.ValueObjects.Role;

public readonly record struct RoleName
{
    public RoleName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(RoleName), 50);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
