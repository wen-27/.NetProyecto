using Domain.ValueObjects;

namespace Domain.ValueObjects.Audit;

public readonly record struct AuditActionTypeId
{
    public AuditActionTypeId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(AuditActionTypeId));
    }
    public int Value { get; }
}
