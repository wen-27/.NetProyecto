// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de AuditActionTypeId, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Audit;

public readonly record struct AuditActionTypeId
{
    public AuditActionTypeId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(AuditActionTypeId));
    }
    public int Value { get; }
}
