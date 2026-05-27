using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Suppliers;

public sealed record GetSupplierById(int Id) : IRequest<SupplierDto>;
public sealed record GetSuppliersPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<SupplierDto>>;

public sealed class GetSupplierByIdHandler : IRequestHandler<GetSupplierById, SupplierDto>
{
    private readonly ISupplierRepository _repository;
    public GetSupplierByIdHandler(ISupplierRepository repository) => _repository = repository;
    public async Task<SupplierDto> Handle(GetSupplierById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Proveedor", request.Id);
}

public sealed class GetSuppliersPagedHandler : IRequestHandler<GetSuppliersPaged, PagedResult<SupplierDto>>
{
    private readonly ISupplierRepository _repository;
    public GetSuppliersPagedHandler(ISupplierRepository repository) => _repository = repository;
    public async Task<PagedResult<SupplierDto>> Handle(GetSuppliersPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<SupplierDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
