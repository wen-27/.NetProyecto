// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de AuditActionTypeName, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.AuditActionType;

public readonly record struct AuditActionTypeName
{
    public AuditActionTypeName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(AuditActionTypeName), 50);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
