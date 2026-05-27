using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Roles;

public sealed record GetRoleById(int Id) : IRequest<RoleDto>;

public sealed record GetRolesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<RoleDto>>;

public sealed class GetRoleByIdHandler : IRequestHandler<GetRoleById, RoleDto>
{
    private readonly IRoleRepository _repository;

    public GetRoleByIdHandler(IRoleRepository repository) => _repository = repository;

    public async Task<RoleDto> Handle(GetRoleById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Rol", request.Id);
    }
}

public sealed class GetRolesPagedHandler : IRequestHandler<GetRolesPaged, PagedResult<RoleDto>>
{
    private readonly IRoleRepository _repository;

    public GetRolesPagedHandler(IRoleRepository repository) => _repository = repository;

    public async Task<PagedResult<RoleDto>> Handle(GetRolesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<RoleDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
