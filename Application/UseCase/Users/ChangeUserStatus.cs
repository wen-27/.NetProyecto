using MediatR;

namespace Application.UseCase.Users;

public sealed record ChangeUserStatus(int Id, bool Status) : IRequest;
