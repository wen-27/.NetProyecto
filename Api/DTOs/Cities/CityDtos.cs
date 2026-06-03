namespace Api.DTOs.Cities;

// DTO usado para transportar datos de CreateCityRequest entre la API y sus consumidores.
public sealed record CreateCityRequest(int DepartmentId, string Name);
// DTO usado para transportar datos de UpdateCityRequest entre la API y sus consumidores.
public sealed record UpdateCityRequest(int DepartmentId, string Name);
// DTO usado para transportar datos de CityResponse entre la API y sus consumidores.
public sealed record CityResponse(int Id, int DepartmentId, string Name);
