using MediatR;

namespace Application.UseCase.UserRoles;

public sealed record RemoveUserRole(int UserId, int RoleId) : IRequest;
