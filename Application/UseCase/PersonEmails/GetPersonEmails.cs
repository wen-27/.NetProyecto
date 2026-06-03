// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GetPersonEmails. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PersonEmails;

public sealed record GetPersonEmailById(int Id) : IRequest<PersonEmailDto>;

public sealed record GetPersonEmailsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PersonEmailDto>>;

public sealed class GetPersonEmailByIdHandler : IRequestHandler<GetPersonEmailById, PersonEmailDto>
{
    private readonly IPersonEmailRepository _repository;

    public GetPersonEmailByIdHandler(IPersonEmailRepository repository) => _repository = repository;

    public async Task<PersonEmailDto> Handle(GetPersonEmailById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Correo de persona", request.Id);
    }
}

public sealed class GetPersonEmailsPagedHandler : IRequestHandler<GetPersonEmailsPaged, PagedResult<PersonEmailDto>>
{
    private readonly IPersonEmailRepository _repository;

    public GetPersonEmailsPagedHandler(IPersonEmailRepository repository) => _repository = repository;

    public async Task<PagedResult<PersonEmailDto>> Handle(GetPersonEmailsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PersonEmailDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
