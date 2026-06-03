// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de RoleName, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Role;

public readonly record struct RoleName
{
    public RoleName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(RoleName), 50);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
