// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de UserDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.Users;

public sealed record CreateUserRequest(int PersonId, string PasswordHash);
public sealed record UpdateUserRequest(int PersonId, string PasswordHash, string? RefreshToken, DateTime? RefreshTokenExpiration, bool IsActive);
public sealed record UserResponse(int Id, int PersonId, string? RefreshToken, DateTime? RefreshTokenExpiration, bool IsActive, DateTime CreatedAt);
