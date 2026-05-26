using Domain.ValueObjects;

namespace Domain.ValueObjects.Audit;

public readonly record struct AuditAffectedEntity
{
    public AuditAffectedEntity(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(AuditAffectedEntity), 100);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
