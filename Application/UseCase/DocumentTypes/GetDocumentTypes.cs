using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.DocumentTypes;

public sealed record GetDocumentTypeById(int Id) : IRequest<DocumentTypeDto>;

public sealed record GetDocumentTypesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<DocumentTypeDto>>;

public sealed class GetDocumentTypeByIdHandler : IRequestHandler<GetDocumentTypeById, DocumentTypeDto>
{
    private readonly IDocumentTypeRepository _repository;

    public GetDocumentTypeByIdHandler(IDocumentTypeRepository repository) => _repository = repository;

    public async Task<DocumentTypeDto> Handle(GetDocumentTypeById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Tipo de documento", request.Id);
    }
}

public sealed class GetDocumentTypesPagedHandler : IRequestHandler<GetDocumentTypesPaged, PagedResult<DocumentTypeDto>>
{
    private readonly IDocumentTypeRepository _repository;

    public GetDocumentTypesPagedHandler(IDocumentTypeRepository repository) => _repository = repository;

    public async Task<PagedResult<DocumentTypeDto>> Handle(GetDocumentTypesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<DocumentTypeDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
