using MediatR;

namespace Application.UseCase.Audits;

// Caso de uso que modela una accion o consulta de negocio relacionada con RegisterAudit.
public sealed record RegisterAudit(
    int UserId,
    int AuditActionTypeId,
    string AffectedEntity,
    int AffectedRecordId,
    string? Description) : IRequest<int>;
