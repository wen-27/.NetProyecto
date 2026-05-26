using Domain.ValueObjects;

namespace Domain.ValueObjects.Audit;

public readonly record struct AuditUserId
{
    public AuditUserId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(AuditUserId));
    }
    public int Value { get; }
}
