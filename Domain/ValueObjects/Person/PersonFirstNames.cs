// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PersonFirstNames, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Person;

public readonly record struct PersonFirstNames
{
    public PersonFirstNames(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PersonFirstNames), 100);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
