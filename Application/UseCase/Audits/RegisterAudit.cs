using MediatR;

namespace Application.UseCase.Audits;

public sealed record RegisterAudit(
    int UserId,
    int AuditActionTypeId,
    string AffectedEntity,
    int AffectedRecordId,
    string? Description) : IRequest<int>;
