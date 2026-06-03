using Domain.ValueObjects;

namespace Domain.ValueObjects.Audit;

// Value Object que encapsula y valida un valor especifico de AuditDescription.
public readonly record struct AuditDescription
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public string? Value { get; }

    public AuditDescription(string? value)
    {
        Value = ValueObjectValidation.Optional(value, nameof(AuditDescription), 5000);
    }

    public bool IsEmpty => string.IsNullOrWhiteSpace(Value);

    public override string ToString() => Value ?? string.Empty;
}
