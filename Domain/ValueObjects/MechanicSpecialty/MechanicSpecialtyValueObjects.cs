using Domain.ValueObjects;

namespace Domain.ValueObjects.MechanicSpecialty;

// Value Object que encapsula y valida un valor especifico de MechanicSpecialtyName.
public readonly record struct MechanicSpecialtyName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public MechanicSpecialtyName(string value) => Value = ValueObjectValidation.Required(value, nameof(MechanicSpecialtyName), 100);
    public string Value { get; }
}
