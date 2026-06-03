using Domain.ValueObjects;

namespace Domain.ValueObjects.Audit;

// Value Object que encapsula y valida un valor especifico de AuditAffectedRecordId.
public readonly record struct AuditAffectedRecordId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public AuditAffectedRecordId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(AuditAffectedRecordId));
    }
    public int Value { get; }
}
