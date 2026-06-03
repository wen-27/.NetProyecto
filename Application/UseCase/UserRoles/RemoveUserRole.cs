using MediatR;

namespace Application.UseCase.UserRoles;

// Caso de uso que modela una accion o consulta de negocio relacionada con RemoveUserRole.
public sealed record RemoveUserRole(int UserId, int RoleId) : IRequest;
