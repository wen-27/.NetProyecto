namespace Api.DTOs.Departments;

public sealed record CreateDepartmentRequest(int CountryId, string Name);
public sealed record UpdateDepartmentRequest(int CountryId, string Name);
public sealed record DepartmentResponse(int Id, int CountryId, string Name);
