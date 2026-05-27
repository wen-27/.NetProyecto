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
