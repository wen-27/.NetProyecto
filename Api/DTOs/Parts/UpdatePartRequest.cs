// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de UpdatePartRequest. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.Parts;

public sealed record UpdatePartRequest(
    int PartCategoryId,
    int? PartBrandId,
    string Code,
    string Description,
    int Stock,
    int MinimumStock,
    decimal UnitPrice,
    bool IsActive);
