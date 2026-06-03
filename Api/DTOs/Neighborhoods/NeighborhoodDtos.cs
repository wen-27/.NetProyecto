// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de NeighborhoodDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.Neighborhoods;

public sealed record CreateNeighborhoodRequest(int CityId, string Name);
public sealed record UpdateNeighborhoodRequest(int CityId, string Name);
public sealed record NeighborhoodResponse(int Id, int CityId, string Name);
