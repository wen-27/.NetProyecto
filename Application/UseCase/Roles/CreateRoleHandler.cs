using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.Role;
using MediatR;

namespace Application.UseCase.Roles;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateRole.
public sealed class CreateRoleHandler : IRequestHandler<CreateRole, int>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IRoleRepository _roles;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleHandler(IRoleRepository roles, IUnitOfWork unitOfWork)
    {
        _roles = roles;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateRole request, CancellationToken ct)
    {
        var roleName = new RoleName(request.RoleName);

        if (await _roles.ExistsNameAsync(roleName, ct))
        {
            throw new InvalidOperationException("Ya existe un rol con ese nombre.");
        }

        var role = new Role { RoleName = roleName.Value };

        await _roles.AddAsync(role, ct);
        await _unitOfWork.CommitAsync(ct);

        return role.Id;
    }
}
