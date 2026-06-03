using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.ServiceTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetServiceTypeById.
public sealed record GetServiceTypeById(int Id) : IRequest<ServiceTypeDto>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetServiceTypesPaged.
public sealed record GetServiceTypesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<ServiceTypeDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetServiceTypeById.
public sealed class GetServiceTypeByIdHandler : IRequestHandler<GetServiceTypeById, ServiceTypeDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IServiceTypeRepository _repository;

    public GetServiceTypeByIdHandler(IServiceTypeRepository repository) => _repository = repository;

    public async Task<ServiceTypeDto> Handle(GetServiceTypeById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Tipo de servicio", request.Id);
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetServiceTypesPaged.
public sealed class GetServiceTypesPagedHandler : IRequestHandler<GetServiceTypesPaged, PagedResult<ServiceTypeDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IServiceTypeRepository _repository;

    public GetServiceTypesPagedHandler(IServiceTypeRepository repository) => _repository = repository;

    public async Task<PagedResult<ServiceTypeDto>> Handle(GetServiceTypesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<ServiceTypeDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
