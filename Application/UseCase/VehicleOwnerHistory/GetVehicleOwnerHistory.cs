// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GetVehicleOwnerHistory. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.VehicleOwnerHistory;

public sealed record GetVehicleOwnerHistoryById(int Id) : IRequest<VehicleOwnerHistoryDto>;

public sealed record GetVehicleOwnerHistoryPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<VehicleOwnerHistoryDto>>;

public sealed class GetVehicleOwnerHistoryByIdHandler : IRequestHandler<GetVehicleOwnerHistoryById, VehicleOwnerHistoryDto>
{
    private readonly IVehicleOwnerHistoryRepository _repository;

    public GetVehicleOwnerHistoryByIdHandler(IVehicleOwnerHistoryRepository repository) => _repository = repository;

    public async Task<VehicleOwnerHistoryDto> Handle(GetVehicleOwnerHistoryById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Historial de propietario", request.Id);
    }
}

public sealed class GetVehicleOwnerHistoryPagedHandler : IRequestHandler<GetVehicleOwnerHistoryPaged, PagedResult<VehicleOwnerHistoryDto>>
{
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
