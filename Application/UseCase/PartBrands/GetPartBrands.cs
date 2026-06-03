// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GetPartBrands. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Application.Common.Exceptions;
using Application.Common.Pagination;
using Application.DTOs;
using MediatR;

namespace Application.UseCase.PartBrands;

public sealed record GetPartBrandById(int Id) : IRequest<PartBrandDto>;
public sealed record GetPartBrandsPaged(int Page = 1, int PageSize = 10, string? Search = null) : IRequest<PagedResult<PartBrandDto>>;

public sealed class GetPartBrandByIdHandler : IRequestHandler<GetPartBrandById, PartBrandDto>
{
    private readonly IPartBrandRepository _repository;
    public GetPartBrandByIdHandler(IPartBrandRepository repository) => _repository = repository;
    public async Task<PartBrandDto> Handle(GetPartBrandById request, CancellationToken ct)
        => (await _repository.GetByIdAsync(request.Id, ct))?.ToDto() ?? throw new NotFoundException("Marca de repuesto", request.Id);
}

public sealed class GetPartBrandsPagedHandler : IRequestHandler<GetPartBrandsPaged, PagedResult<PartBrandDto>>
{
    private readonly IPartBrandRepository _repository;
    public GetPartBrandsPagedHandler(IPartBrandRepository repository) => _repository = repository;
    public async Task<PagedResult<PartBrandDto>> Handle(GetPartBrandsPaged request, CancellationToken ct)
    {
        var pagination = new PaginationRequest(request.Page, request.PageSize, request.Search);
        var items = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize, pagination.Search, ct);
        var total = await _repository.CountAsync(pagination.Search, ct);
        return new PagedResult<PartBrandDto>(items.Select(x => x.ToDto()).ToArray(), total, pagination.Page, pagination.PageSize);
    }
}
