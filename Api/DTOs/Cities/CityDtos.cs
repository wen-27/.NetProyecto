// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de CityDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.Cities;

public sealed record CreateCityRequest(int DepartmentId, string Name);
public sealed record UpdateCityRequest(int DepartmentId, string Name);
public sealed record CityResponse(int Id, int DepartmentId, string Name);
