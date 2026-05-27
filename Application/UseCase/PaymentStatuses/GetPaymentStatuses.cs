using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PaymentStatuses;

public sealed record GetPaymentStatusById(int Id) : IRequest<PaymentStatusDto>;
public sealed record GetPaymentStatusesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PaymentStatusDto>>;

public sealed class GetPaymentStatusByIdHandler : IRequestHandler<GetPaymentStatusById, PaymentStatusDto>
{
    private readonly IPaymentStatusRepository _repository;
    public GetPaymentStatusByIdHandler(IPaymentStatusRepository repository) => _repository = repository;
    public async Task<PaymentStatusDto> Handle(GetPaymentStatusById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Estado de pago", request.Id);
}

public sealed class GetPaymentStatusesPagedHandler : IRequestHandler<GetPaymentStatusesPaged, PagedResult<PaymentStatusDto>>
{
    private readonly IPaymentStatusRepository _repository;
    public GetPaymentStatusesPagedHandler(IPaymentStatusRepository repository) => _repository = repository;
    public async Task<PagedResult<PaymentStatusDto>> Handle(GetPaymentStatusesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PaymentStatusDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
