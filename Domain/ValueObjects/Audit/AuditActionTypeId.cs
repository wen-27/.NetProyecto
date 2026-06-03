using Domain.ValueObjects;

namespace Domain.ValueObjects.Audit;

// Value Object que encapsula y valida un valor especifico de AuditActionTypeId.
public readonly record struct AuditActionTypeId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public AuditActionTypeId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(AuditActionTypeId));
    }
    public int Value { get; }
}
