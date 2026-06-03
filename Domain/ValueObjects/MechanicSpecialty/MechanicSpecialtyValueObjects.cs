// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de MechanicSpecialtyValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.MechanicSpecialty;

public readonly record struct MechanicSpecialtyName
{
    public MechanicSpecialtyName(string value) => Value = ValueObjectValidation.Required(value, nameof(MechanicSpecialtyName), 100);
    public string Value { get; }
}
