// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GetCities. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.Cities;

public sealed record GetCityById(int Id) : IRequest<CityDto>;
public sealed record GetCitiesPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<CityDto>>;

public sealed class GetCityByIdHandler : IRequestHandler<GetCityById, CityDto>
{
    private readonly ICityRepository _repository;
    public GetCityByIdHandler(ICityRepository repository) => _repository = repository;
    public async Task<CityDto> Handle(GetCityById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Ciudad", request.Id);
}

public sealed class GetCitiesPagedHandler : IRequestHandler<GetCitiesPaged, PagedResult<CityDto>>
{
    private readonly ICityRepository _repository;
    public GetCitiesPagedHandler(ICityRepository repository) => _repository = repository;
    public async Task<PagedResult<CityDto>> Handle(GetCitiesPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<CityDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
