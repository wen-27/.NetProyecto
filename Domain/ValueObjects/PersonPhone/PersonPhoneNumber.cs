// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PersonPhoneNumber, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonPhone;

public readonly record struct PersonPhoneNumber
{
    public PersonPhoneNumber(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PersonPhoneNumber), 30);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
