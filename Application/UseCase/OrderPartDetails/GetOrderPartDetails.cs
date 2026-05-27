using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.OrderPartDetails;

public sealed record GetOrderPartDetailById(int Id) : IRequest<OrderPartDetailDto>;

public sealed record GetOrderPartDetailsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<OrderPartDetailDto>>;

public sealed class GetOrderPartDetailByIdHandler : IRequestHandler<GetOrderPartDetailById, OrderPartDetailDto>
{
    private readonly IOrderPartDetailRepository _repository;

    public GetOrderPartDetailByIdHandler(IOrderPartDetailRepository repository) => _repository = repository;

    public async Task<OrderPartDetailDto> Handle(GetOrderPartDetailById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Detalle de repuesto de orden", request.Id);
    }
}

public sealed class GetOrderPartDetailsPagedHandler : IRequestHandler<GetOrderPartDetailsPaged, PagedResult<OrderPartDetailDto>>
{
    private readonly IOrderPartDetailRepository _repository;

    public GetOrderPartDetailsPagedHandler(IOrderPartDetailRepository repository) => _repository = repository;

    public async Task<PagedResult<OrderPartDetailDto>> Handle(GetOrderPartDetailsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<OrderPartDetailDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
