// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de AuditAffectedRecordId, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Audit;

public readonly record struct AuditAffectedRecordId
{
    public AuditAffectedRecordId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(AuditAffectedRecordId));
    }
    public int Value { get; }
}
