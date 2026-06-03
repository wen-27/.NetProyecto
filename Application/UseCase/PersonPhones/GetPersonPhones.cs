using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PersonPhones;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPersonPhoneById.
public sealed record GetPersonPhoneById(int Id) : IRequest<PersonPhoneDto>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPersonPhonesPaged.
public sealed record GetPersonPhonesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PersonPhoneDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPersonPhoneById.
public sealed class GetPersonPhoneByIdHandler : IRequestHandler<GetPersonPhoneById, PersonPhoneDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPersonPhoneRepository _repository;

    public GetPersonPhoneByIdHandler(IPersonPhoneRepository repository) => _repository = repository;

    public async Task<PersonPhoneDto> Handle(GetPersonPhoneById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Teléfono de persona", request.Id);
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPersonPhonesPaged.
public sealed class GetPersonPhonesPagedHandler : IRequestHandler<GetPersonPhonesPaged, PagedResult<PersonPhoneDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPersonPhoneRepository _repository;

    public GetPersonPhonesPagedHandler(IPersonPhoneRepository repository) => _repository = repository;

    public async Task<PagedResult<PersonPhoneDto>> Handle(GetPersonPhonesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PersonPhoneDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
