using MediatR;

namespace Application.UseCase.Users;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateUser.
public sealed record CreateUser(int PersonId, string PasswordHash) : IRequest<int>;
