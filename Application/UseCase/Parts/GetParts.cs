using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Parts;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartById.
public sealed record GetPartById(int Id) : IRequest<PartDto>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartsPaged.
public sealed record GetPartsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PartDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartById.
public sealed class GetPartByIdHandler : IRequestHandler<GetPartById, PartDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPartRepository _repository;

    public GetPartByIdHandler(IPartRepository repository) => _repository = repository;

    public async Task<PartDto> Handle(GetPartById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Repuesto", request.Id);
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartsPaged.
public sealed class GetPartsPagedHandler : IRequestHandler<GetPartsPaged, PagedResult<PartDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPartRepository _repository;

    public GetPartsPagedHandler(IPartRepository repository) => _repository = repository;

    public async Task<PagedResult<PartDto>> Handle(GetPartsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PartDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
