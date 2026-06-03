using Domain.ValueObjects;

namespace Domain.ValueObjects.User;

// Value Object que encapsula y valida un valor especifico de UserRefreshToken.
public readonly record struct UserRefreshToken
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public UserRefreshToken(string? value) => Value = value;
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de UserRefreshTokenExpiration.
public readonly record struct UserRefreshTokenExpiration
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public UserRefreshTokenExpiration(DateTime? value) => Value = value;
    public DateTime? Value { get; }
}
