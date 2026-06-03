// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de MechanicSpecialtyAssignmentValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.MechanicSpecialtyAssignment;

public readonly record struct MechanicSpecialtyAssignmentPersonId
{
    public MechanicSpecialtyAssignmentPersonId(int value) => Value = ValueObjectValidation.Positive(value, nameof(MechanicSpecialtyAssignmentPersonId));
    public int Value { get; }
}

public readonly record struct MechanicSpecialtyAssignmentSpecialtyId
{
    public MechanicSpecialtyAssignmentSpecialtyId(int value) => Value = ValueObjectValidation.Positive(value, nameof(MechanicSpecialtyAssignmentSpecialtyId));
    public int Value { get; }
}
