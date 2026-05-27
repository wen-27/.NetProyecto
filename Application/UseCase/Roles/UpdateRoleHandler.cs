using Application.Abstractions;
using Domain.ValueObjects.Role;
using MediatR;

namespace Application.UseCase.Roles;

public sealed class UpdateRoleHandler : IRequestHandler<UpdateRole>
{
    private readonly IRoleRepository _roles;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoleHandler(IRoleRepository roles, IUnitOfWork unitOfWork)
    {
        _roles = roles;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateRole request, CancellationToken ct)
    {
        var role = await _roles.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el rol.");

        var roleName = new RoleName(request.RoleName);
        role.RoleName = roleName.Value;

        await _roles.UpdateAsync(role, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
