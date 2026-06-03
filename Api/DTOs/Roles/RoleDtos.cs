namespace Api.DTOs.Roles;

// DTO usado para transportar datos de CreateRoleRequest entre la API y sus consumidores.
public sealed record CreateRoleRequest(string RoleName);
// DTO usado para transportar datos de RoleResponse entre la API y sus consumidores.
public sealed record RoleResponse(int Id, string RoleName);
