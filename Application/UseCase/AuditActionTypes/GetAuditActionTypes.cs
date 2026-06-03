// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GetAuditActionTypes. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.AuditActionTypes;

public sealed record GetAuditActionTypeById(int Id) : IRequest<AuditActionTypeDto>;

public sealed record GetAuditActionTypesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<AuditActionTypeDto>>;

public sealed class GetAuditActionTypeByIdHandler : IRequestHandler<GetAuditActionTypeById, AuditActionTypeDto>
{
    private readonly IAuditActionTypeRepository _repository;

    public GetAuditActionTypeByIdHandler(IAuditActionTypeRepository repository) => _repository = repository;

    public async Task<AuditActionTypeDto> Handle(GetAuditActionTypeById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Tipo de acción de auditoría", request.Id);
    }
}

public sealed class GetAuditActionTypesPagedHandler : IRequestHandler<GetAuditActionTypesPaged, PagedResult<AuditActionTypeDto>>
{
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
