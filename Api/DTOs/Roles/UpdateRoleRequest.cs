namespace Api.DTOs.Roles;

// DTO usado para transportar datos de UpdateRoleRequest entre la API y sus consumidores.
public sealed record UpdateRoleRequest(string RoleName);
