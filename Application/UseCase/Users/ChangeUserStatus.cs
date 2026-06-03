using MediatR;

namespace Application.UseCase.Users;

// Caso de uso que modela una accion o consulta de negocio relacionada con ChangeUserStatus.
public sealed record ChangeUserStatus(int Id, bool Status) : IRequest;
