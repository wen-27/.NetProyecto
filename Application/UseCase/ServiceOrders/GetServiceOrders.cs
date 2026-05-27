using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.ServiceOrders;

public sealed record GetServiceOrderById(int Id) : IRequest<ServiceOrderDto>;

public sealed record GetServiceOrdersPaged(
    int Page = 1,
    int PageSize = 10,
    string? Search = null,
    int? ClientPersonId = null,
    string? Vin = null,
    DateTime? FromDate = null,
    DateTime? ToDate = null,
    int? StatusId = null,
    int? MechanicPersonId = null) : IRequest<PagedResult<ServiceOrderDto>>;

public sealed class GetServiceOrderByIdHandler : IRequestHandler<GetServiceOrderById, ServiceOrderDto>
{
    private readonly IServiceOrderRepository _repository;

    public GetServiceOrderByIdHandler(IServiceOrderRepository repository) => _repository = repository;

    public async Task<ServiceOrderDto> Handle(GetServiceOrderById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Orden de servicio", request.Id);
    }
}

public sealed class GetServiceOrdersPagedHandler : IRequestHandler<GetServiceOrdersPaged, PagedResult<ServiceOrderDto>>
{
    private readonly IServiceOrderRepository _repository;

    public GetServiceOrdersPagedHandler(IServiceOrderRepository repository) => _repository = repository;

    public async Task<PagedResult<ServiceOrderDto>> Handle(GetServiceOrdersPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetFilteredAsync(
            pagination.Page,
            pagination.PageSize,
            pagination.Search,
            request.ClientPersonId,
            request.Vin,
            request.FromDate,
            request.ToDate,
            request.StatusId,
            request.MechanicPersonId,
            ct);
        var total = await _repository.CountFilteredAsync(
            pagination.Search,
            request.ClientPersonId,
            request.Vin,
            request.FromDate,
            request.ToDate,
            request.StatusId,
            request.MechanicPersonId,
            ct);
        return new PagedResult<ServiceOrderDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
