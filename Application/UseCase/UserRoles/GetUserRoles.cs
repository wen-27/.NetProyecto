using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using Domain.ValueObjects.UserRole;
using MediatR;

namespace Application.UseCase.UserRoles;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetUserRoleByIds.
public sealed record GetUserRoleByIds(int UserId, int RoleId) : IRequest<UserRoleDto>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetUserRolesPaged.
public sealed record GetUserRolesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<UserRoleDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetUserRoleByIds.
public sealed class GetUserRoleByIdsHandler : IRequestHandler<GetUserRoleByIds, UserRoleDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
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

// Caso de uso que modela una accion o consulta de negocio relacionada con GetUserRolesPaged.
public sealed class GetUserRolesPagedHandler : IRequestHandler<GetUserRolesPaged, PagedResult<UserRoleDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
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
