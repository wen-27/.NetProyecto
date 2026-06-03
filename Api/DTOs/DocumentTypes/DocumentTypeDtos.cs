// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de DocumentTypeDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.DocumentTypes;

public sealed record CreateDocumentTypeRequest(string Code, string Name);
public sealed record DocumentTypeResponse(int Id, string Code, string Name);
