using Domain.ValueObjects;

namespace Domain.ValueObjects.MechanicAssignment;

// Value Object que encapsula y valida un valor especifico de MechanicAssignmentOrderServiceId.
public readonly record struct MechanicAssignmentOrderServiceId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public MechanicAssignmentOrderServiceId(int value) => Value = ValueObjectValidation.Positive(value, nameof(MechanicAssignmentOrderServiceId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de MechanicAssignmentMechanicPersonId.
public readonly record struct MechanicAssignmentMechanicPersonId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public MechanicAssignmentMechanicPersonId(int value) => Value = ValueObjectValidation.Positive(value, nameof(MechanicAssignmentMechanicPersonId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de MechanicAssignmentSpecialtyId.
public readonly record struct MechanicAssignmentSpecialtyId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public MechanicAssignmentSpecialtyId(int value) => Value = ValueObjectValidation.Positive(value, nameof(MechanicAssignmentSpecialtyId));
    public int Value { get; }
}
