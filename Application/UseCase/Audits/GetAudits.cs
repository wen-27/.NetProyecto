using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Audits;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetAuditById.
public sealed record GetAuditById(int Id) : IRequest<AuditDto>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetAuditsPaged.
public sealed record GetAuditsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<AuditDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetAuditById.
public sealed class GetAuditByIdHandler : IRequestHandler<GetAuditById, AuditDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IAuditRepository _repository;

    public GetAuditByIdHandler(IAuditRepository repository) => _repository = repository;

    public async Task<AuditDto> Handle(GetAuditById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Auditoría", request.Id);
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetAuditsPaged.
public sealed class GetAuditsPagedHandler : IRequestHandler<GetAuditsPaged, PagedResult<AuditDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IAuditRepository _repository;

    public GetAuditsPagedHandler(IAuditRepository repository) => _repository = repository;

    public async Task<PagedResult<AuditDto>> Handle(GetAuditsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<AuditDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
