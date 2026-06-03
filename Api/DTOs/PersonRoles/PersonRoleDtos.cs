// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de PersonRoleDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.PersonRoles;

public sealed record CreatePersonRoleRequest(int PersonId, int RoleId, bool IsActive = true);
public sealed record UpdatePersonRoleRequest(int PersonId, int RoleId, bool IsActive);
public sealed record PersonRoleResponse(int Id, int PersonId, int RoleId, bool IsActive);
