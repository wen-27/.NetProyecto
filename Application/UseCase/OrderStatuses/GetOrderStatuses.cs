using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.OrderStatuses;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetOrderStatusById.
public sealed record GetOrderStatusById(int Id) : IRequest<OrderStatusDto>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetOrderStatusesPaged.
public sealed record GetOrderStatusesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<OrderStatusDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetOrderStatusById.
public sealed class GetOrderStatusByIdHandler : IRequestHandler<GetOrderStatusById, OrderStatusDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IOrderStatusRepository _repository;

    public GetOrderStatusByIdHandler(IOrderStatusRepository repository) => _repository = repository;

    public async Task<OrderStatusDto> Handle(GetOrderStatusById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Estado de orden", request.Id);
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetOrderStatusesPaged.
public sealed class GetOrderStatusesPagedHandler : IRequestHandler<GetOrderStatusesPaged, PagedResult<OrderStatusDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IOrderStatusRepository _repository;

    public GetOrderStatusesPagedHandler(IOrderStatusRepository repository) => _repository = repository;

    public async Task<PagedResult<OrderStatusDto>> Handle(GetOrderStatusesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<OrderStatusDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
