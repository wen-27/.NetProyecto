using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Users;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetUserById.
public sealed record GetUserById(int Id) : IRequest<UserDto>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetUsersPaged.
public sealed record GetUsersPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<UserDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetUserById.
public sealed class GetUserByIdHandler : IRequestHandler<GetUserById, UserDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IUserRepository _repository;

    public GetUserByIdHandler(IUserRepository repository) => _repository = repository;

    public async Task<UserDto> Handle(GetUserById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Usuario", request.Id);
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetUsersPaged.
public sealed class GetUsersPagedHandler : IRequestHandler<GetUsersPaged, PagedResult<UserDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IUserRepository _repository;

    public GetUsersPagedHandler(IUserRepository repository) => _repository = repository;

    public async Task<PagedResult<UserDto>> Handle(GetUsersPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<UserDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
