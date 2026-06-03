using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PersonEmails;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPersonEmailById.
public sealed record GetPersonEmailById(int Id) : IRequest<PersonEmailDto>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPersonEmailsPaged.
public sealed record GetPersonEmailsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PersonEmailDto>>;

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPersonEmailById.
public sealed class GetPersonEmailByIdHandler : IRequestHandler<GetPersonEmailById, PersonEmailDto>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPersonEmailRepository _repository;

    public GetPersonEmailByIdHandler(IPersonEmailRepository repository) => _repository = repository;

    public async Task<PersonEmailDto> Handle(GetPersonEmailById request, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(request.Id, ct);
        return entity?.ToDto() ?? throw new NotFoundException("Correo de persona", request.Id);
    }
}

// Caso de uso que modela una accion o consulta de negocio relacionada con GetPersonEmailsPaged.
public sealed class GetPersonEmailsPagedHandler : IRequestHandler<GetPersonEmailsPaged, PagedResult<PersonEmailDto>>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
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
