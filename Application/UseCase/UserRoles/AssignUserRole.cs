using MediatR;

namespace Application.UseCase.UserRoles;

// Caso de uso que modela una accion o consulta de negocio relacionada con AssignUserRole.
public sealed record AssignUserRole(int UserId, int RoleId) : IRequest;
