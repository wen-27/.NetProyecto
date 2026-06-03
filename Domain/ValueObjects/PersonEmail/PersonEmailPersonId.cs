// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PersonEmailPersonId, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonEmail;

public readonly record struct PersonEmailPersonId
{
    public PersonEmailPersonId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(PersonEmailPersonId));
    }
    public int Value { get; }
}
