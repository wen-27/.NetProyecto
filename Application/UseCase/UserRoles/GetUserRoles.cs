// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GetUserRoles. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using Domain.ValueObjects.UserRole;
using MediatR;

namespace Application.UseCase.UserRoles;

public sealed record GetUserRoleByIds(int UserId, int RoleId) : IRequest<UserRoleDto>;

public sealed record GetUserRolesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<UserRoleDto>>;

public sealed class GetUserRoleByIdsHandler : IRequestHandler<GetUserRoleByIds, UserRoleDto>
{
    private readonly IUserRoleRepository _repository;

    public GetUserRoleByIdsHandler(IUserRoleRepository repository) => _repository = repository;

    public async Task<UserRoleDto> Handle(GetUserRoleByIds request, CancellationToken ct)
    {
        var userId = new UserRoleUserId(request.UserId);
        var roleId = new UserRoleRoleId(request.RoleId);
        var entity = await _repository.GetByIdsAsync(userId, roleId, ct);
        return entity?.ToDto() ?? throw new NotFoundException($"No se encontró el rol {request.RoleId} asignado al usuario {request.UserId}.");
    }
}

public sealed class GetUserRolesPagedHandler : IRequestHandler<GetUserRolesPaged, PagedResult<UserRoleDto>>
{
    private readonly IUserRoleRepository _repository;

    public GetUserRolesPagedHandler(IUserRoleRepository repository) => _repository = repository;

    public async Task<PagedResult<UserRoleDto>> Handle(GetUserRolesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<UserRoleDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
