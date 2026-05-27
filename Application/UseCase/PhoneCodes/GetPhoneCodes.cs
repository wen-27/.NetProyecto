using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PhoneCodes;

public sealed record GetPhoneCodeById(int Id) : IRequest<PhoneCodeDto>;

public sealed record GetPhoneCodesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PhoneCodeDto>>;

public sealed class GetPhoneCodeByIdHandler : IRequestHandler<GetPhoneCodeById, PhoneCodeDto>
{
    private readonly IPhoneCodeRepository _repository;

    public GetPhoneCodeByIdHandler(IPhoneCodeRepository repository) => _repository = repository;

    public async Task<PhoneCodeDto> Handle(GetPhoneCodeById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Código telefónico", request.Id);
    }
}

public sealed class GetPhoneCodesPagedHandler : IRequestHandler<GetPhoneCodesPaged, PagedResult<PhoneCodeDto>>
{
    private readonly IPhoneCodeRepository _repository;

    public GetPhoneCodesPagedHandler(IPhoneCodeRepository repository) => _repository = repository;

    public async Task<PagedResult<PhoneCodeDto>> Handle(GetPhoneCodesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PhoneCodeDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
