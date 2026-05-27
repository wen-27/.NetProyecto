namespace Api.DTOs.Roles;

public sealed record CreateRoleRequest(string RoleName);
public sealed record RoleResponse(int Id, string RoleName);
