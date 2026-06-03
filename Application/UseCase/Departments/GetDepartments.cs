using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Departments;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetDepartmentById.
public sealed record GetDepartmentById(int Id) : IRequest<DepartmentDto>;
// Caso de uso que modela una accion o consulta de negocio relacionada con GetDepartmentsPaged.
public sealed record GetDepartmentsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<DepartmentDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetDepartmentById.
public sealed class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentById, DepartmentDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IDepartmentRepository _repository;
    public GetDepartmentByIdHandler(IDepartmentRepository repository) => _repository = repository;
    public async Task<DepartmentDto> Handle(GetDepartmentById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Departamento", request.Id);
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetDepartmentsPaged.
public sealed class GetDepartmentsPagedHandler : IRequestHandler<GetDepartmentsPaged, PagedResult<DepartmentDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IDepartmentRepository _repository;
    public GetDepartmentsPagedHandler(IDepartmentRepository repository) => _repository = repository;
    public async Task<PagedResult<DepartmentDto>> Handle(GetDepartmentsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<DepartmentDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
