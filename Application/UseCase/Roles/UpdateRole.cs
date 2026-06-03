using MediatR;

namespace Application.UseCase.Roles;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateRole.
public sealed record UpdateRole(int Id, string RoleName) : IRequest;
