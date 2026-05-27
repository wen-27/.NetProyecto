using MediatR;

namespace Application.UseCase.AuditActionTypes;

public sealed record CreateAuditActionType(string Name) : IRequest<int>;
