// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PersonEmailUser, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonEmail;

public readonly record struct PersonEmailUser
{
    public PersonEmailUser(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PersonEmailUser), 100);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
