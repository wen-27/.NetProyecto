using MediatR;

namespace Application.UseCase.AuditActionTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateAuditActionType.
public sealed record UpdateAuditActionType(int Id, string Name) : IRequest;
