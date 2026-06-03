// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de RoleDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.Roles;

public sealed record CreateRoleRequest(string RoleName);
public sealed record RoleResponse(int Id, string RoleName);
