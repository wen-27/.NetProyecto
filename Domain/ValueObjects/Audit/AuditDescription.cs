using Domain.ValueObjects;

namespace Domain.ValueObjects.Audit;

public readonly record struct AuditDescription
{
    public string? Value { get; }

    public AuditDescription(string? value)
    {
        Value = ValueObjectValidation.Optional(value, nameof(AuditDescription), 5000);
    }

    public bool IsEmpty => string.IsNullOrWhiteSpace(Value);

    public override string ToString() => Value ?? string.Empty;
}
