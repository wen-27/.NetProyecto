// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de MechanicAssignmentValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.MechanicAssignment;

public readonly record struct MechanicAssignmentOrderServiceId
{
    public MechanicAssignmentOrderServiceId(int value) => Value = ValueObjectValidation.Positive(value, nameof(MechanicAssignmentOrderServiceId));
    public int Value { get; }
}

public readonly record struct MechanicAssignmentMechanicPersonId
{
    public MechanicAssignmentMechanicPersonId(int value) => Value = ValueObjectValidation.Positive(value, nameof(MechanicAssignmentMechanicPersonId));
    public int Value { get; }
}

public readonly record struct MechanicAssignmentSpecialtyId
{
    public MechanicAssignmentSpecialtyId(int value) => Value = ValueObjectValidation.Positive(value, nameof(MechanicAssignmentSpecialtyId));
    public int Value { get; }
}
