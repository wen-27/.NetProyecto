namespace Api.DTOs.Auth;

public sealed record LoginResponse(
    int UserId,
    int PersonId,
    string Email,
    string Role,
    string AccessToken,
    DateTime ExpiresAt);
