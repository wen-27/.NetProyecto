using Domain.ValueObjects;

namespace Domain.ValueObjects.MechanicSpecialtyAssignment;

// Value Object que encapsula y valida un valor especifico de MechanicSpecialtyAssignmentPersonId.
public readonly record struct MechanicSpecialtyAssignmentPersonId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public MechanicSpecialtyAssignmentPersonId(int value) => Value = ValueObjectValidation.Positive(value, nameof(MechanicSpecialtyAssignmentPersonId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de MechanicSpecialtyAssignmentSpecialtyId.
public readonly record struct MechanicSpecialtyAssignmentSpecialtyId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public MechanicSpecialtyAssignmentSpecialtyId(int value) => Value = ValueObjectValidation.Positive(value, nameof(MechanicSpecialtyAssignmentSpecialtyId));
    public int Value { get; }
}
