using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.VehicleModels;

public sealed record GetVehicleModelById(int Id) : IRequest<VehicleModelDto>;

public sealed record GetVehicleModelsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<VehicleModelDto>>;

public sealed class GetVehicleModelByIdHandler : IRequestHandler<GetVehicleModelById, VehicleModelDto>
{
    private readonly IVehicleModelRepository _repository;

    public GetVehicleModelByIdHandler(IVehicleModelRepository repository) => _repository = repository;

    public async Task<VehicleModelDto> Handle(GetVehicleModelById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Modelo de vehículo", request.Id);
    }
}

public sealed class GetVehicleModelsPagedHandler : IRequestHandler<GetVehicleModelsPaged, PagedResult<VehicleModelDto>>
{
    private readonly IVehicleModelRepository _repository;

    public GetVehicleModelsPagedHandler(IVehicleModelRepository repository) => _repository = repository;

    public async Task<PagedResult<VehicleModelDto>> Handle(GetVehicleModelsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<VehicleModelDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
