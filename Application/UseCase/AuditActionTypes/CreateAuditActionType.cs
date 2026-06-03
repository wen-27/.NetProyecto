using MediatR;

namespace Application.UseCase.AuditActionTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateAuditActionType.
public sealed record CreateAuditActionType(string Name) : IRequest<int>;
