using MediatR;

namespace Application.UseCase.Roles;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateRole.
public sealed record CreateRole(string RoleName) : IRequest<int>;
