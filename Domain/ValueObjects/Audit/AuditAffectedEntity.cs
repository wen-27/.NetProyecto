using Domain.ValueObjects;

namespace Domain.ValueObjects.Audit;

// Value Object que encapsula y valida un valor especifico de AuditAffectedEntity.
public readonly record struct AuditAffectedEntity
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public AuditAffectedEntity(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(AuditAffectedEntity), 100);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
