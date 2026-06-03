// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de StreetTypeDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.StreetTypes;

public sealed record CreateStreetTypeRequest(string Name);
public sealed record UpdateStreetTypeRequest(string Name);
public sealed record StreetTypeResponse(int Id, string Name);
