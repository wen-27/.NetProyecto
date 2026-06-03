using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PartPurchases;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartPurchaseById.
public sealed record GetPartPurchaseById(int Id) : IRequest<PartPurchaseDto>;
// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartPurchasesPaged.
public sealed record GetPartPurchasesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PartPurchaseDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartPurchaseById.
public sealed class GetPartPurchaseByIdHandler : IRequestHandler<GetPartPurchaseById, PartPurchaseDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPartPurchaseRepository _repository;
    public GetPartPurchaseByIdHandler(IPartPurchaseRepository repository) => _repository = repository;
    public async Task<PartPurchaseDto> Handle(GetPartPurchaseById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Compra de repuesto", request.Id);
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPartPurchasesPaged.
public sealed class GetPartPurchasesPagedHandler : IRequestHandler<GetPartPurchasesPaged, PagedResult<PartPurchaseDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPartPurchaseRepository _repository;
    public GetPartPurchasesPagedHandler(IPartPurchaseRepository repository) => _repository = repository;
    public async Task<PagedResult<PartPurchaseDto>> Handle(GetPartPurchasesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PartPurchaseDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
