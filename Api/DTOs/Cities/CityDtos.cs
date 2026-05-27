namespace Api.DTOs.Cities;

public sealed record CreateCityRequest(int DepartmentId, string Name);
public sealed record UpdateCityRequest(int DepartmentId, string Name);
public sealed record CityResponse(int Id, int DepartmentId, string Name);
