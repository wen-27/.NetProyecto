// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de UserNewSchemaValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.User;

public readonly record struct UserRefreshToken
{
    public UserRefreshToken(string? value) => Value = value;
    public string? Value { get; }
}

public readonly record struct UserRefreshTokenExpiration
{
    public UserRefreshTokenExpiration(DateTime? value) => Value = value;
    public DateTime? Value { get; }
}
