using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.CardTypes;

public sealed record GetCardTypeById(int Id) : IRequest<CardTypeDto>;
public sealed record GetCardTypesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<CardTypeDto>>;

public sealed class GetCardTypeByIdHandler : IRequestHandler<GetCardTypeById, CardTypeDto>
{
    private readonly ICardTypeRepository _repository;
    public GetCardTypeByIdHandler(ICardTypeRepository repository) => _repository = repository;
    public async Task<CardTypeDto> Handle(GetCardTypeById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Tipo de tarjeta", request.Id);
}

public sealed class GetCardTypesPagedHandler : IRequestHandler<GetCardTypesPaged, PagedResult<CardTypeDto>>
{
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
