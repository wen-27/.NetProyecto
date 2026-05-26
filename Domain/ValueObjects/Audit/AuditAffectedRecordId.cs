using Domain.ValueObjects;

namespace Domain.ValueObjects.Audit;

public readonly record struct AuditAffectedRecordId
{
    public AuditAffectedRecordId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(AuditAffectedRecordId));
    }
    public int Value { get; }
}
