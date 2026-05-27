using MediatR;

namespace Application.UseCase.Users;

public sealed record CreateUser(int PersonId, string PasswordHash) : IRequest<int>;
