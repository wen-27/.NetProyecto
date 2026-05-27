namespace Api.DTOs.Users;

public sealed record CreateUserRequest(int PersonId, string PasswordHash);
public sealed record UpdateUserRequest(int PersonId, string PasswordHash, string? RefreshToken, DateTime? RefreshTokenExpiration, bool IsActive);
public sealed record UserResponse(int Id, int PersonId, string? RefreshToken, DateTime? RefreshTokenExpiration, bool IsActive, DateTime CreatedAt);
