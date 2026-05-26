using Domain.ValueObjects;

namespace Domain.ValueObjects.AuditActionType;

public readonly record struct AuditActionTypeName
{
    public AuditActionTypeName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(AuditActionTypeName), 50);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
