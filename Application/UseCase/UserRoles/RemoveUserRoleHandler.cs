using Application.Abstractions;
using Domain.ValueObjects.UserRole;
using MediatR;

namespace Application.UseCase.UserRoles;

// Caso de uso que modela una accion o consulta de negocio relacionada con RemoveUserRole.
public sealed class RemoveUserRoleHandler : IRequestHandler<RemoveUserRole>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IUserRoleRepository _userRoles;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveUserRoleHandler(IUserRoleRepository userRoles, IUnitOfWork unitOfWork)
    {
        _userRoles = userRoles;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveUserRole request, CancellationToken ct)
    {
        var userId = new UserRoleUserId(request.UserId);
        var roleId = new UserRoleRoleId(request.RoleId);
        var userRole = await _userRoles.GetByIdsAsync(userId, roleId, ct)
            ?? throw new KeyNotFoundException("No se encontró la asignación de rol.");

        await _userRoles.RemoveAsync(userRole, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
