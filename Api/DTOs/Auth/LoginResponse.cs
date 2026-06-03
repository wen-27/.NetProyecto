// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de LoginResponse. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.Auth;

public sealed record LoginResponse(
    int UserId,
    int PersonId,
    string Email,
    string Role,
    string AccessToken,
    DateTime ExpiresAt);
