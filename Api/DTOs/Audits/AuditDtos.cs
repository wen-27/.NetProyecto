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
