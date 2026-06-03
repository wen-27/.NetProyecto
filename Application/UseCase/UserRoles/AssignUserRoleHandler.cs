// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con AssignUserRoleHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.UserRole;
using MediatR;

namespace Application.UseCase.UserRoles;

public sealed class AssignUserRoleHandler : IRequestHandler<AssignUserRole>
{
    private readonly IUserRoleRepository _userRoles;
    private readonly IUnitOfWork _unitOfWork;

    public AssignUserRoleHandler(IUserRoleRepository userRoles, IUnitOfWork unitOfWork)
    {
        _userRoles = userRoles;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AssignUserRole request, CancellationToken ct)
    {
        var userId = new UserRoleUserId(request.UserId);
        var roleId = new UserRoleRoleId(request.RoleId);

        if (await _userRoles.ExistsAsync(userId, roleId, ct))
        {
            throw new InvalidOperationException("El usuario ya tiene asignado ese rol.");
        }

        await _userRoles.AddAsync(new UserRole { UserId = userId.Value, RoleId = roleId.Value }, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
