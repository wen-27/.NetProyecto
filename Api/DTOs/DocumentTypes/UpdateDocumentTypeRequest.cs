// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de UpdateDocumentTypeRequest. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.DocumentTypes;

public sealed record UpdateDocumentTypeRequest(string Code, string Name);
