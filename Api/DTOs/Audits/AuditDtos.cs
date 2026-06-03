namespace Api.DTOs.Audits;

// DTO usado para transportar datos de CreateAuditRequest entre la API y sus consumidores.
public sealed record CreateAuditRequest(
    int UserId,
    int AuditActionTypeId,
    string AffectedEntity,
    int AffectedRecordId,
    string? Description);

// DTO usado para transportar datos de AuditResponse entre la API y sus consumidores.
public sealed record AuditResponse(
    int Id,
    int UserId,
    int AuditActionTypeId,
    string AffectedEntity,
    int AffectedRecordId,
    string? Description,
    DateTime CreatedAt);
