using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PaymentMethods;

public sealed record GetPaymentMethodById(int Id) : IRequest<PaymentMethodDto>;
public sealed record GetPaymentMethodsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PaymentMethodDto>>;

public sealed class GetPaymentMethodByIdHandler : IRequestHandler<GetPaymentMethodById, PaymentMethodDto>
{
    private readonly IPaymentMethodRepository _repository;
    public GetPaymentMethodByIdHandler(IPaymentMethodRepository repository) => _repository = repository;
    public async Task<PaymentMethodDto> Handle(GetPaymentMethodById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Método de pago", request.Id);
}

public sealed class GetPaymentMethodsPagedHandler : IRequestHandler<GetPaymentMethodsPaged, PagedResult<PaymentMethodDto>>
{
    private readonly IPaymentMethodRepository _repository;
    public GetPaymentMethodsPagedHandler(IPaymentMethodRepository repository) => _repository = repository;
    public async Task<PagedResult<PaymentMethodDto>> Handle(GetPaymentMethodsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PaymentMethodDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
