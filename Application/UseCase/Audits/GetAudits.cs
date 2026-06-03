// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GetAudits. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Audits;

public sealed record GetAuditById(int Id) : IRequest<AuditDto>;

public sealed record GetAuditsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<AuditDto>>;

public sealed class GetAuditByIdHandler : IRequestHandler<GetAuditById, AuditDto>
{
    private readonly IAuditRepository _repository;

    public GetAuditByIdHandler(IAuditRepository repository) => _repository = repository;

    public async Task<AuditDto> Handle(GetAuditById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Auditoría", request.Id);
    }
}

public sealed class GetAuditsPagedHandler : IRequestHandler<GetAuditsPaged, PagedResult<AuditDto>>
{
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
