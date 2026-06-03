using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.CardTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetCardTypeById.
public sealed record GetCardTypeById(int Id) : IRequest<CardTypeDto>;
// Caso de uso que modela una accion o consulta de negocio relacionada con GetCardTypesPaged.
public sealed record GetCardTypesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<CardTypeDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetCardTypeById.
public sealed class GetCardTypeByIdHandler : IRequestHandler<GetCardTypeById, CardTypeDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly ICardTypeRepository _repository;
    public GetCardTypeByIdHandler(ICardTypeRepository repository) => _repository = repository;
    public async Task<CardTypeDto> Handle(GetCardTypeById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Tipo de tarjeta", request.Id);
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetCardTypesPaged.
public sealed class GetCardTypesPagedHandler : IRequestHandler<GetCardTypesPaged, PagedResult<CardTypeDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly ICardTypeRepository _repository;
    public GetCardTypesPagedHandler(ICardTypeRepository repository) => _repository = repository;
    public async Task<PagedResult<CardTypeDto>> Handle(GetCardTypesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<CardTypeDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
