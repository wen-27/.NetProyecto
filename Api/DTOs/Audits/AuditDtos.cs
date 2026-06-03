// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de AuditDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.Audits;

public sealed record CreateAuditRequest(
    int UserId,
    int AuditActionTypeId,
    string AffectedEntity,
    int AffectedRecordId,
    string? Description);

public sealed record AuditResponse(
    int Id,
    int UserId,
    int AuditActionTypeId,
    string AffectedEntity,
    int AffectedRecordId,
    string? Description,
    DateTime CreatedAt);
