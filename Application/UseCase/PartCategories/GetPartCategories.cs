using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PartCategories;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartCategoryById.
public sealed record GetPartCategoryById(int Id) : IRequest<PartCategoryDto>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartCategoriesPaged.
public sealed record GetPartCategoriesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PartCategoryDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartCategoryById.
public sealed class GetPartCategoryByIdHandler : IRequestHandler<GetPartCategoryById, PartCategoryDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPartCategoryRepository _repository;

    public GetPartCategoryByIdHandler(IPartCategoryRepository repository) => _repository = repository;

    public async Task<PartCategoryDto> Handle(GetPartCategoryById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Categoría de repuesto", request.Id);
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartCategoriesPaged.
public sealed class GetPartCategoriesPagedHandler : IRequestHandler<GetPartCategoriesPaged, PagedResult<PartCategoryDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPartCategoryRepository _repository;

    public GetPartCategoriesPagedHandler(IPartCategoryRepository repository) => _repository = repository;

    public async Task<PagedResult<PartCategoryDto>> Handle(GetPartCategoriesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PartCategoryDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
