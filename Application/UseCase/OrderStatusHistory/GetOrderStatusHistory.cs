using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.OrderStatusHistory;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetOrderStatusHistoryById.
public sealed record GetOrderStatusHistoryById(int Id) : IRequest<OrderStatusHistoryDto>;
// Caso de uso que modela una accion o consulta de negocio relacionada con GetOrderStatusHistoryPaged.
public sealed record GetOrderStatusHistoryPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<OrderStatusHistoryDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetOrderStatusHistoryById.
public sealed class GetOrderStatusHistoryByIdHandler : IRequestHandler<GetOrderStatusHistoryById, OrderStatusHistoryDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IOrderStatusHistoryRepository _repository;
    public GetOrderStatusHistoryByIdHandler(IOrderStatusHistoryRepository repository) => _repository = repository;
    public async Task<OrderStatusHistoryDto> Handle(GetOrderStatusHistoryById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Historial de estado de orden", request.Id);
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetOrderStatusHistoryPaged.
public sealed class GetOrderStatusHistoryPagedHandler : IRequestHandler<GetOrderStatusHistoryPaged, PagedResult<OrderStatusHistoryDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IOrderStatusHistoryRepository _repository;
    public GetOrderStatusHistoryPagedHandler(IOrderStatusHistoryRepository repository) => _repository = repository;
    public async Task<PagedResult<OrderStatusHistoryDto>> Handle(GetOrderStatusHistoryPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<OrderStatusHistoryDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
