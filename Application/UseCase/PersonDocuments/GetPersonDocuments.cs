using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PersonDocuments;

public sealed record GetPersonDocumentById(int Id) : IRequest<PersonDocumentDto>;

public sealed record GetPersonDocumentsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PersonDocumentDto>>;

public sealed class GetPersonDocumentByIdHandler : IRequestHandler<GetPersonDocumentById, PersonDocumentDto>
{
    private readonly IPersonDocumentRepository _repository;

    public GetPersonDocumentByIdHandler(IPersonDocumentRepository repository) => _repository = repository;

    public async Task<PersonDocumentDto> Handle(GetPersonDocumentById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Documento de persona", request.Id);
    }
}

public sealed class GetPersonDocumentsPagedHandler : IRequestHandler<GetPersonDocumentsPaged, PagedResult<PersonDocumentDto>>
{
    private readonly IPersonDocumentRepository _repository;

    public GetPersonDocumentsPagedHandler(IPersonDocumentRepository repository) => _repository = repository;

    public async Task<PagedResult<PersonDocumentDto>> Handle(GetPersonDocumentsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PersonDocumentDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
