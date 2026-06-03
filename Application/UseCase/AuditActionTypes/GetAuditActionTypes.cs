using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.AuditActionTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetAuditActionTypeById.
public sealed record GetAuditActionTypeById(int Id) : IRequest<AuditActionTypeDto>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetAuditActionTypesPaged.
public sealed record GetAuditActionTypesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<AuditActionTypeDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetAuditActionTypeById.
public sealed class GetAuditActionTypeByIdHandler : IRequestHandler<GetAuditActionTypeById, AuditActionTypeDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IAuditActionTypeRepository _repository;

    public GetAuditActionTypeByIdHandler(IAuditActionTypeRepository repository) => _repository = repository;

    public async Task<AuditActionTypeDto> Handle(GetAuditActionTypeById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Tipo de acción de auditoría", request.Id);
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetAuditActionTypesPaged.
public sealed class GetAuditActionTypesPagedHandler : IRequestHandler<GetAuditActionTypesPaged, PagedResult<AuditActionTypeDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IAuditActionTypeRepository _repository;

    public GetAuditActionTypesPagedHandler(IAuditActionTypeRepository repository) => _repository = repository;

    public async Task<PagedResult<AuditActionTypeDto>> Handle(GetAuditActionTypesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<AuditActionTypeDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
