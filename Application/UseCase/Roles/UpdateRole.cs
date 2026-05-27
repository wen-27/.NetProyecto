using MediatR;

namespace Application.UseCase.Roles;

public sealed record UpdateRole(int Id, string RoleName) : IRequest;
