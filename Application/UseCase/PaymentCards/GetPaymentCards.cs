// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GetPaymentCards. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PaymentCards;

public sealed record GetPaymentCardById(int Id) : IRequest<PaymentCardDto>;
public sealed record GetPaymentCardsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PaymentCardDto>>;

public sealed class GetPaymentCardByIdHandler : IRequestHandler<GetPaymentCardById, PaymentCardDto>
{
    private readonly IPaymentCardRepository _repository;
    public GetPaymentCardByIdHandler(IPaymentCardRepository repository) => _repository = repository;
    public async Task<PaymentCardDto> Handle(GetPaymentCardById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Tarjeta de pago", request.Id);
}

public sealed class GetPaymentCardsPagedHandler : IRequestHandler<GetPaymentCardsPaged, PagedResult<PaymentCardDto>>
{
    private readonly IPaymentCardRepository _repository;
    public GetPaymentCardsPagedHandler(IPaymentCardRepository repository) => _repository = repository;
    public async Task<PagedResult<PaymentCardDto>> Handle(GetPaymentCardsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PaymentCardDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
