// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GetPersonPhones. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PersonPhones;

public sealed record GetPersonPhoneById(int Id) : IRequest<PersonPhoneDto>;

public sealed record GetPersonPhonesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PersonPhoneDto>>;

public sealed class GetPersonPhoneByIdHandler : IRequestHandler<GetPersonPhoneById, PersonPhoneDto>
{
    private readonly IPersonPhoneRepository _repository;

    public GetPersonPhoneByIdHandler(IPersonPhoneRepository repository) => _repository = repository;

    public async Task<PersonPhoneDto> Handle(GetPersonPhoneById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Teléfono de persona", request.Id);
    }
}

public sealed class GetPersonPhonesPagedHandler : IRequestHandler<GetPersonPhonesPaged, PagedResult<PersonPhoneDto>>
{
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
