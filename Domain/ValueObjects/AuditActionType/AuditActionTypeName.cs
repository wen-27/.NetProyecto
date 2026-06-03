using Domain.ValueObjects;

namespace Domain.ValueObjects.AuditActionType;

// Value Object que encapsula y valida un valor especifico de AuditActionTypeName.
public readonly record struct AuditActionTypeName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public AuditActionTypeName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(AuditActionTypeName), 50);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
