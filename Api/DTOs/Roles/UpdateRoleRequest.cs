// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de UpdateRoleRequest. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.Roles;

public sealed record UpdateRoleRequest(string RoleName);
