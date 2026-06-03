// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GetDepartments. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Departments;

public sealed record GetDepartmentById(int Id) : IRequest<DepartmentDto>;
public sealed record GetDepartmentsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<DepartmentDto>>;

public sealed class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentById, DepartmentDto>
{
    private readonly IDepartmentRepository _repository;
    public GetDepartmentByIdHandler(IDepartmentRepository repository) => _repository = repository;
    public async Task<DepartmentDto> Handle(GetDepartmentById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Departamento", request.Id);
}

public sealed class GetDepartmentsPagedHandler : IRequestHandler<GetDepartmentsPaged, PagedResult<DepartmentDto>>
{
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
