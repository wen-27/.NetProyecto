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
