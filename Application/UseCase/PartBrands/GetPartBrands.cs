using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PartBrands;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartBrandById.
public sealed record GetPartBrandById(int Id) : IRequest<PartBrandDto>;
// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartBrandsPaged.
public sealed record GetPartBrandsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PartBrandDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartBrandById.
public sealed class GetPartBrandByIdHandler : IRequestHandler<GetPartBrandById, PartBrandDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPartBrandRepository _repository;
    public GetPartBrandByIdHandler(IPartBrandRepository repository) => _repository = repository;
    public async Task<PartBrandDto> Handle(GetPartBrandById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Marca de repuesto", request.Id);
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartBrandsPaged.
public sealed class GetPartBrandsPagedHandler : IRequestHandler<GetPartBrandsPaged, PagedResult<PartBrandDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPartBrandRepository _repository;
    public GetPartBrandsPagedHandler(IPartBrandRepository repository) => _repository = repository;
    public async Task<PagedResult<PartBrandDto>> Handle(GetPartBrandsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PartBrandDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
