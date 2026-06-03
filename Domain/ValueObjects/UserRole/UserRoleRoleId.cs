// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de UserRoleRoleId, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.UserRole;

public readonly record struct UserRoleRoleId
{
    public UserRoleRoleId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(UserRoleRoleId));
    }
    public int Value { get; }
}
