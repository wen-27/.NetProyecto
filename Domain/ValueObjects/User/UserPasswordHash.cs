// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de UserPasswordHash, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.User;

public readonly record struct UserPasswordHash
{
    public UserPasswordHash(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(UserPasswordHash), 255);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
