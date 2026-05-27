using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Customers;

public sealed record GetCustomerById(int Id) : IRequest<CustomerDto>;

public sealed record GetCustomersPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<CustomerDto>>;

public sealed class GetCustomerByIdHandler : IRequestHandler<GetCustomerById, CustomerDto>
{
    private readonly ICustomerRepository _repository;

    public GetCustomerByIdHandler(ICustomerRepository repository) => _repository = repository;

    public async Task<CustomerDto> Handle(GetCustomerById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Cliente", request.Id);
    }
}

public sealed class GetCustomersPagedHandler : IRequestHandler<GetCustomersPaged, PagedResult<CustomerDto>>
{
    private readonly ICustomerRepository _repository;

    public GetCustomersPagedHandler(ICustomerRepository repository) => _repository = repository;

    public async Task<PagedResult<CustomerDto>> Handle(GetCustomersPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<CustomerDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
