using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.VehicleBrands;

public sealed record GetVehicleBrandById(int Id) : IRequest<VehicleBrandDto>;

public sealed record GetVehicleBrandsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<VehicleBrandDto>>;

public sealed class GetVehicleBrandByIdHandler : IRequestHandler<GetVehicleBrandById, VehicleBrandDto>
{
    private readonly IVehicleBrandRepository _repository;

    public GetVehicleBrandByIdHandler(IVehicleBrandRepository repository) => _repository = repository;

    public async Task<VehicleBrandDto> Handle(GetVehicleBrandById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Marca de vehículo", request.Id);
    }
}

public sealed class GetVehicleBrandsPagedHandler : IRequestHandler<GetVehicleBrandsPaged, PagedResult<VehicleBrandDto>>
{
    private readonly IVehicleBrandRepository _repository;

    public GetVehicleBrandsPagedHandler(IVehicleBrandRepository repository) => _repository = repository;

    public async Task<PagedResult<VehicleBrandDto>> Handle(GetVehicleBrandsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<VehicleBrandDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
