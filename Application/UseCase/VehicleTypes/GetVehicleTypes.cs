using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.VehicleTypes;

public sealed record GetVehicleTypeById(int Id) : IRequest<VehicleTypeDto>;
public sealed record GetVehicleTypesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<VehicleTypeDto>>;

public sealed class GetVehicleTypeByIdHandler : IRequestHandler<GetVehicleTypeById, VehicleTypeDto>
{
    private readonly IVehicleTypeRepository _repository;
    public GetVehicleTypeByIdHandler(IVehicleTypeRepository repository) => _repository = repository;
    public async Task<VehicleTypeDto> Handle(GetVehicleTypeById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Tipo de vehículo", request.Id);
}

public sealed class GetVehicleTypesPagedHandler : IRequestHandler<GetVehicleTypesPaged, PagedResult<VehicleTypeDto>>
{
    private readonly IVehicleTypeRepository _repository;
    public GetVehicleTypesPagedHandler(IVehicleTypeRepository repository) => _repository = repository;
    public async Task<PagedResult<VehicleTypeDto>> Handle(GetVehicleTypesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<VehicleTypeDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
