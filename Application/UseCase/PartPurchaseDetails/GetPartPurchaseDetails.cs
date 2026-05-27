using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PartPurchaseDetails;

public sealed record GetPartPurchaseDetailById(int Id) : IRequest<PartPurchaseDetailDto>;
public sealed record GetPartPurchaseDetailsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PartPurchaseDetailDto>>;

public sealed class GetPartPurchaseDetailByIdHandler : IRequestHandler<GetPartPurchaseDetailById, PartPurchaseDetailDto>
{
    private readonly IPartPurchaseDetailRepository _repository;
    public GetPartPurchaseDetailByIdHandler(IPartPurchaseDetailRepository repository) => _repository = repository;
    public async Task<PartPurchaseDetailDto> Handle(GetPartPurchaseDetailById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Detalle de compra de repuesto", request.Id);
}

public sealed class GetPartPurchaseDetailsPagedHandler : IRequestHandler<GetPartPurchaseDetailsPaged, PagedResult<PartPurchaseDetailDto>>
{
    private readonly IPartPurchaseDetailRepository _repository;
    public GetPartPurchaseDetailsPagedHandler(IPartPurchaseDetailRepository repository) => _repository = repository;
    public async Task<PagedResult<PartPurchaseDetailDto>> Handle(GetPartPurchaseDetailsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PartPurchaseDetailDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
