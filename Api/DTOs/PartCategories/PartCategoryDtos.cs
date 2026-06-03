// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de PartCategoryDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.PartCategories;

public sealed record CreatePartCategoryRequest(string Name);
public sealed record PartCategoryResponse(int Id, string Name);
