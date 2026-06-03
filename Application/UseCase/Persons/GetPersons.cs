using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Persons;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPersonById.
public sealed record GetPersonById(int Id) : IRequest<PersonDto>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPersonsPaged.
public sealed record GetPersonsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PersonDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPersonById.
public sealed class GetPersonByIdHandler : IRequestHandler<GetPersonById, PersonDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPersonRepository _repository;

    public GetPersonByIdHandler(IPersonRepository repository) => _repository = repository;

    public async Task<PersonDto> Handle(GetPersonById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Persona", request.Id);
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPersonsPaged.
public sealed class GetPersonsPagedHandler : IRequestHandler<GetPersonsPaged, PagedResult<PersonDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
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
