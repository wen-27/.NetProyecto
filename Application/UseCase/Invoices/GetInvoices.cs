// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GetInvoices. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Invoices;

public sealed record GetInvoiceById(int Id) : IRequest<InvoiceDto>;

public sealed record GetInvoicesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<InvoiceDto>>;

public sealed class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceById, InvoiceDto>
{
    private readonly IInvoiceRepository _repository;

    public GetInvoiceByIdHandler(IInvoiceRepository repository) => _repository = repository;

    public async Task<InvoiceDto> Handle(GetInvoiceById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Factura", request.Id);
    }
}

public sealed class GetInvoicesPagedHandler : IRequestHandler<GetInvoicesPaged, PagedResult<InvoiceDto>>
{
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
