using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PersonAddresses;

public sealed record GetPersonAddressById(int Id) : IRequest<PersonAddressDto>;
public sealed record GetPersonAddressesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PersonAddressDto>>;

public sealed class GetPersonAddressByIdHandler : IRequestHandler<GetPersonAddressById, PersonAddressDto>
{
    private readonly IPersonAddressRepository _repository;
    public GetPersonAddressByIdHandler(IPersonAddressRepository repository) => _repository = repository;
    public async Task<PersonAddressDto> Handle(GetPersonAddressById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Dirección de persona", request.Id);
}

public sealed class GetPersonAddressesPagedHandler : IRequestHandler<GetPersonAddressesPaged, PagedResult<PersonAddressDto>>
{
    private readonly IPersonAddressRepository _repository;
    public GetPersonAddressesPagedHandler(IPersonAddressRepository repository) => _repository = repository;
    public async Task<PagedResult<PersonAddressDto>> Handle(GetPersonAddressesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PersonAddressDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
