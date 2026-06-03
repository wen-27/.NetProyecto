using Domain.ValueObjects;

namespace Domain.ValueObjects.Audit;

// Value Object que encapsula y valida un valor especifico de AuditUserId.
public readonly record struct AuditUserId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public AuditUserId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(AuditUserId));
    }
    public int Value { get; }
}
