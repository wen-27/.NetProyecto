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
