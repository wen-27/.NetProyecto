namespace Api.DTOs.Users;

// DTO usado para transportar datos de CreateUserRequest entre la API y sus consumidores.
public sealed record CreateUserRequest(int PersonId, string PasswordHash);
// DTO usado para transportar datos de UpdateUserRequest entre la API y sus consumidores.
public sealed record UpdateUserRequest(int PersonId, string PasswordHash, string? RefreshToken, DateTime? RefreshTokenExpiration, bool IsActive);
// DTO usado para transportar datos de UserResponse entre la API y sus consumidores.
public sealed record UserResponse(int Id, int PersonId, string? RefreshToken, DateTime? RefreshTokenExpiration, bool IsActive, DateTime CreatedAt);
