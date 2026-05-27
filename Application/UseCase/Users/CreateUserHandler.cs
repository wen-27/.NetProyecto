using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.User;
using MediatR;

namespace Application.UseCase.Users;

public sealed class CreateUserHandler : IRequestHandler<CreateUser, int>
{
    private readonly IUserRepository _users;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(IUserRepository users, IUnitOfWork unitOfWork)
    {
        _users = users;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateUser request, CancellationToken ct)
    {
        var personId = new UserPersonId(request.PersonId);
        var passwordHash = new UserPasswordHash(request.PasswordHash);

        if (await _users.ExistsPersonIdAsync(personId, ct))
        {
            throw new InvalidOperationException("Ya existe un usuario asociado a esa persona.");
        }

        var user = new User
        {
            PersonId = personId.Value,
            PasswordHash = passwordHash.Value,
            Status = UserStatus.Active().IsActive
        };

        await _users.AddAsync(user, ct);
        await _unitOfWork.CommitAsync(ct);

        return user.Id;
    }
}
