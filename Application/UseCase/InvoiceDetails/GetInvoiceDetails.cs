using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.InvoiceDetails;

public sealed record GetInvoiceDetailById(int Id) : IRequest<InvoiceDetailDto>;

public sealed record GetInvoiceDetailsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<InvoiceDetailDto>>;

public sealed class GetInvoiceDetailByIdHandler : IRequestHandler<GetInvoiceDetailById, InvoiceDetailDto>
{
    private readonly IInvoiceDetailRepository _repository;

    public GetInvoiceDetailByIdHandler(IInvoiceDetailRepository repository) => _repository = repository;

    public async Task<InvoiceDetailDto> Handle(GetInvoiceDetailById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Detalle de factura", request.Id);
    }
}

public sealed class GetInvoiceDetailsPagedHandler : IRequestHandler<GetInvoiceDetailsPaged, PagedResult<InvoiceDetailDto>>
{
    private readonly IInvoiceDetailRepository _repository;

    public GetInvoiceDetailsPagedHandler(IInvoiceDetailRepository repository) => _repository = repository;

    public async Task<PagedResult<InvoiceDetailDto>> Handle(GetInvoiceDetailsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<InvoiceDetailDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
