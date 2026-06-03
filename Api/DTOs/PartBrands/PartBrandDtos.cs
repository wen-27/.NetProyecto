// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de PartBrandDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.PartBrands;

public sealed record CreatePartBrandRequest(string Name);
public sealed record UpdatePartBrandRequest(string Name);
public sealed record PartBrandResponse(int Id, string Name);
