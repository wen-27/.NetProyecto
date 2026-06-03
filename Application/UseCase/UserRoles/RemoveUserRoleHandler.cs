// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con RemoveUserRoleHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.ValueObjects.UserRole;
using MediatR;

namespace Application.UseCase.UserRoles;

public sealed class RemoveUserRoleHandler : IRequestHandler<RemoveUserRole>
{
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
