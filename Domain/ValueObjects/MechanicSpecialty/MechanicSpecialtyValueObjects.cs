using Domain.ValueObjects;

namespace Domain.ValueObjects.MechanicSpecialty;

public readonly record struct MechanicSpecialtyName
{
    public MechanicSpecialtyName(string value) => Value = ValueObjectValidation.Required(value, nameof(MechanicSpecialtyName), 100);
    public string Value { get; }
}
