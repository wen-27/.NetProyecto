using MediatR;

namespace Application.UseCase.AuditActionTypes;

public sealed record UpdateAuditActionType(int Id, string Name) : IRequest;
