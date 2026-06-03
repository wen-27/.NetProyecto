using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Invoices;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetInvoiceById.
public sealed record GetInvoiceById(int Id) : IRequest<InvoiceDto>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetInvoicesPaged.
public sealed record GetInvoicesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<InvoiceDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetInvoiceById.
public sealed class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceById, InvoiceDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IInvoiceRepository _repository;

    public GetInvoiceByIdHandler(IInvoiceRepository repository) => _repository = repository;

    public async Task<InvoiceDto> Handle(GetInvoiceById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Factura", request.Id);
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetInvoicesPaged.
public sealed class GetInvoicesPagedHandler : IRequestHandler<GetInvoicesPaged, PagedResult<InvoiceDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IInvoiceRepository _repository;

    public GetInvoicesPagedHandler(IInvoiceRepository repository) => _repository = repository;

    public async Task<PagedResult<InvoiceDto>> Handle(GetInvoicesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<InvoiceDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
