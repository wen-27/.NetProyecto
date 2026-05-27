using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.ServiceOrderServices;

public sealed record GetServiceOrderServiceById(int Id) : IRequest<ServiceOrderServiceDto>;

public sealed record GetServiceOrderServicesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<ServiceOrderServiceDto>>;

public sealed class GetServiceOrderServiceByIdHandler : IRequestHandler<GetServiceOrderServiceById, ServiceOrderServiceDto>
{
    private readonly IServiceOrderServiceRepository _repository;

    public GetServiceOrderServiceByIdHandler(IServiceOrderServiceRepository repository) => _repository = repository;

    public async Task<ServiceOrderServiceDto> Handle(GetServiceOrderServiceById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto()
            ?? throw new NotFoundException("Servicio de orden", request.Id);
}

public sealed class GetServiceOrderServicesPagedHandler : IRequestHandler<GetServiceOrderServicesPaged, PagedResult<ServiceOrderServiceDto>>
{
    private readonly IServiceOrderServiceRepository _repository;

    public GetServiceOrderServicesPagedHandler(IServiceOrderServiceRepository repository) => _repository = repository;

    public async Task<PagedResult<ServiceOrderServiceDto>> Handle(GetServiceOrderServicesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<ServiceOrderServiceDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
