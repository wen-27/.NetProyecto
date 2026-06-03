using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Vehicles;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetVehicleById.
public sealed record GetVehicleById(int Id) : IRequest<VehicleDto>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetVehiclesPaged.
public sealed record GetVehiclesPaged(
    int Page = 1,
    int PageSize = 10,
    string? Search = null,
    string? Vin = null,
    int? ClientPersonId = null) : IRequest<PagedResult<VehicleDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetVehicleById.
public sealed class GetVehicleByIdHandler : IRequestHandler<GetVehicleById, VehicleDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IVehicleRepository _repository;

    public GetVehicleByIdHandler(IVehicleRepository repository) => _repository = repository;

    public async Task<VehicleDto> Handle(GetVehicleById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Vehículo", request.Id);
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetVehiclesPaged.
public sealed class GetVehiclesPagedHandler : IRequestHandler<GetVehiclesPaged, PagedResult<VehicleDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IVehicleRepository _repository;

    public GetVehiclesPagedHandler(IVehicleRepository repository) => _repository = repository;

    public async Task<PagedResult<VehicleDto>> Handle(GetVehiclesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetFilteredAsync(pagination.Page, pagination.PageSize, pagination.Search, request.Vin, request.ClientPersonId, ct);
        var total = await _repository.CountFilteredAsync(pagination.Search, request.Vin, request.ClientPersonId, ct);
        return new PagedResult<VehicleDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
