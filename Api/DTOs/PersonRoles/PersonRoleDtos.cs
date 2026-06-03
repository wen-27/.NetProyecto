namespace Api.DTOs.PersonRoles;

// DTO usado para transportar datos de CreatePersonRoleRequest entre la API y sus consumidores.
public sealed record CreatePersonRoleRequest(int PersonId, int RoleId, bool IsActive = true);
// DTO usado para transportar datos de UpdatePersonRoleRequest entre la API y sus consumidores.
public sealed record UpdatePersonRoleRequest(int PersonId, int RoleId, bool IsActive);
// DTO usado para transportar datos de PersonRoleResponse entre la API y sus consumidores.
public sealed record PersonRoleResponse(int Id, int PersonId, int RoleId, bool IsActive);
