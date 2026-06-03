// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PersonPhonePersonId, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonPhone;

public readonly record struct PersonPhonePersonId
{
    public PersonPhonePersonId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(PersonPhonePersonId));
    }
    public int Value { get; }
}
