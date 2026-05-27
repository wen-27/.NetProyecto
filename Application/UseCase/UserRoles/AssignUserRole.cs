using MediatR;

namespace Application.UseCase.UserRoles;

public sealed record AssignUserRole(int UserId, int RoleId) : IRequest;
