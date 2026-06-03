// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GetPayments. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Payments;

public sealed record GetPaymentById(int Id) : IRequest<PaymentDto>;
public sealed record GetPaymentsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PaymentDto>>;

public sealed class GetPaymentByIdHandler : IRequestHandler<GetPaymentById, PaymentDto>
{
    private readonly IPaymentRepository _repository;
    public GetPaymentByIdHandler(IPaymentRepository repository) => _repository = repository;
    public async Task<PaymentDto> Handle(GetPaymentById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Pago", request.Id);
}

public sealed class GetPaymentsPagedHandler : IRequestHandler<GetPaymentsPaged, PagedResult<PaymentDto>>
{
    private readonly IPaymentRepository _repository;
    public GetPaymentsPagedHandler(IPaymentRepository repository) => _repository = repository;
    public async Task<PagedResult<PaymentDto>> Handle(GetPaymentsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PaymentDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
