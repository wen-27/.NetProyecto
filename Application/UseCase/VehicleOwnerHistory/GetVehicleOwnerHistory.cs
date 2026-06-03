using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.VehicleOwnerHistory;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetVehicleOwnerHistoryById.
public sealed record GetVehicleOwnerHistoryById(int Id) : IRequest<VehicleOwnerHistoryDto>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetVehicleOwnerHistoryPaged.
public sealed record GetVehicleOwnerHistoryPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<VehicleOwnerHistoryDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetVehicleOwnerHistoryById.
public sealed class GetVehicleOwnerHistoryByIdHandler : IRequestHandler<GetVehicleOwnerHistoryById, VehicleOwnerHistoryDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IVehicleOwnerHistoryRepository _repository;

    public GetVehicleOwnerHistoryByIdHandler(IVehicleOwnerHistoryRepository repository) => _repository = repository;

    public async Task<VehicleOwnerHistoryDto> Handle(GetVehicleOwnerHistoryById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Historial de propietario", request.Id);
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetVehicleOwnerHistoryPaged.
public sealed class GetVehicleOwnerHistoryPagedHandler : IRequestHandler<GetVehicleOwnerHistoryPaged, PagedResult<VehicleOwnerHistoryDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IVehicleOwnerHistoryRepository _repository;

    public GetVehicleOwnerHistoryPagedHandler(IVehicleOwnerHistoryRepository repository) => _repository = repository;

    public async Task<PagedResult<VehicleOwnerHistoryDto>> Handle(GetVehicleOwnerHistoryPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<VehicleOwnerHistoryDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
