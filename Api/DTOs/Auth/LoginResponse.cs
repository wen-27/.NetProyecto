namespace Api.DTOs.Auth;

// DTO usado para transportar datos de LoginResponse entre la API y sus consumidores.
public sealed record LoginResponse(
    int UserId,
    int PersonId,
    string Email,
    string Role,
    string AccessToken,
    DateTime ExpiresAt);
