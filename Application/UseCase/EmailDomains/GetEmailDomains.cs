using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.EmailDomains;

public sealed record GetEmailDomainById(int Id) : IRequest<EmailDomainDto>;

public sealed record GetEmailDomainsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<EmailDomainDto>>;

public sealed class GetEmailDomainByIdHandler : IRequestHandler<GetEmailDomainById, EmailDomainDto>
{
    private readonly IEmailDomainRepository _repository;

    public GetEmailDomainByIdHandler(IEmailDomainRepository repository) => _repository = repository;

    public async Task<EmailDomainDto> Handle(GetEmailDomainById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Dominio de correo", request.Id);
    }
}

public sealed class GetEmailDomainsPagedHandler : IRequestHandler<GetEmailDomainsPaged, PagedResult<EmailDomainDto>>
{
    private readonly IEmailDomainRepository _repository;

    public GetEmailDomainsPagedHandler(IEmailDomainRepository repository) => _repository = repository;

    public async Task<PagedResult<EmailDomainDto>> Handle(GetEmailDomainsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<EmailDomainDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
