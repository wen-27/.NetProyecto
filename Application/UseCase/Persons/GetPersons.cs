// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GetPersons. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Persons;

public sealed record GetPersonById(int Id) : IRequest<PersonDto>;

public sealed record GetPersonsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PersonDto>>;

public sealed class GetPersonByIdHandler : IRequestHandler<GetPersonById, PersonDto>
{
    private readonly IPersonRepository _repository;

    public GetPersonByIdHandler(IPersonRepository repository) => _repository = repository;

    public async Task<PersonDto> Handle(GetPersonById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Persona", request.Id);
    }
}

public sealed class GetPersonsPagedHandler : IRequestHandler<GetPersonsPaged, PagedResult<PersonDto>>
{
    private readonly IPersonRepository _repository;

    public GetPersonsPagedHandler(IPersonRepository repository) => _repository = repository;

    public async Task<PagedResult<PersonDto>> Handle(GetPersonsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PersonDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
