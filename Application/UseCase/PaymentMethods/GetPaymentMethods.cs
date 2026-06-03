using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PaymentMethods;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPaymentMethodById.
public sealed record GetPaymentMethodById(int Id) : IRequest<PaymentMethodDto>;
// Caso de uso que modela una accion o consulta de negocio relacionada con GetPaymentMethodsPaged.
public sealed record GetPaymentMethodsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PaymentMethodDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPaymentMethodById.
public sealed class GetPaymentMethodByIdHandler : IRequestHandler<GetPaymentMethodById, PaymentMethodDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPaymentMethodRepository _repository;
    public GetPaymentMethodByIdHandler(IPaymentMethodRepository repository) => _repository = repository;
    public async Task<PaymentMethodDto> Handle(GetPaymentMethodById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Método de pago", request.Id);
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPaymentMethodsPaged.
public sealed class GetPaymentMethodsPagedHandler : IRequestHandler<GetPaymentMethodsPaged, PagedResult<PaymentMethodDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
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
