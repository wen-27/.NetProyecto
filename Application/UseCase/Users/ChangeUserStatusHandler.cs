using Application.Abstractions;
using Domain.ValueObjects.User;
using MediatR;

namespace Application.UseCase.Users;

// Caso de uso que modela una accion o consulta de negocio relacionada con ChangeUserStatus.
public sealed class ChangeUserStatusHandler : IRequestHandler<ChangeUserStatus>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IUserRepository _users;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeUserStatusHandler(IUserRepository users, IUnitOfWork unitOfWork)
    {
        _users = users;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ChangeUserStatus request, CancellationToken ct)
    {
        var user = await _users.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el usuario.");

        var status = new UserStatus(request.Status);
        user.Status = status.IsActive;

        await _users.UpdateAsync(user, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
