// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GetInvoiceStatuses. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.InvoiceStatuses;

public sealed record GetInvoiceStatusById(int Id) : IRequest<InvoiceStatusDto>;
public sealed record GetInvoiceStatusesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<InvoiceStatusDto>>;

public sealed class GetInvoiceStatusByIdHandler : IRequestHandler<GetInvoiceStatusById, InvoiceStatusDto>
{
    private readonly IInvoiceStatusRepository _repository;
    public GetInvoiceStatusByIdHandler(IInvoiceStatusRepository repository) => _repository = repository;
    public async Task<InvoiceStatusDto> Handle(GetInvoiceStatusById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Estado de factura", request.Id);
}

public sealed class GetInvoiceStatusesPagedHandler : IRequestHandler<GetInvoiceStatusesPaged, PagedResult<InvoiceStatusDto>>
{
    private readonly IInvoiceStatusRepository _repository;
    public GetInvoiceStatusesPagedHandler(IInvoiceStatusRepository repository) => _repository = repository;
    public async Task<PagedResult<InvoiceStatusDto>> Handle(GetInvoiceStatusesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<InvoiceStatusDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
