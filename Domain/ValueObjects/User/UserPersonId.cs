// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de UserPersonId, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.User;

public readonly record struct UserPersonId
{
    public UserPersonId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(UserPersonId));
    }
    public int Value { get; }
}
