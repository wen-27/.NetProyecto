// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de AuditActionTypeDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.AuditActionTypes;

public sealed record CreateAuditActionTypeRequest(string Name);
public sealed record AuditActionTypeResponse(int Id, string Name);
