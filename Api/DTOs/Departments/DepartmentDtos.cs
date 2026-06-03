namespace Api.DTOs.Departments;

// DTO usado para transportar datos de CreateDepartmentRequest entre la API y sus consumidores.
public sealed record CreateDepartmentRequest(int CountryId, string Name);
// DTO usado para transportar datos de UpdateDepartmentRequest entre la API y sus consumidores.
public sealed record UpdateDepartmentRequest(int CountryId, string Name);
// DTO usado para transportar datos de DepartmentResponse entre la API y sus consumidores.
public sealed record DepartmentResponse(int Id, int CountryId, string Name);
