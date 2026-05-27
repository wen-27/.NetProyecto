namespace Api.DTOs.PersonRoles;

public sealed record CreatePersonRoleRequest(int PersonId, int RoleId, bool IsActive = true);
public sealed record UpdatePersonRoleRequest(int PersonId, int RoleId, bool IsActive);
public sealed record PersonRoleResponse(int Id, int PersonId, int RoleId, bool IsActive);
