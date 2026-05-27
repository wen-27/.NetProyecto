using MediatR;

namespace Application.UseCase.Roles;

public sealed record CreateRole(string RoleName) : IRequest<int>;
