// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con GetOrderServices. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Application.Common.Pagination;
using Application.UseCase.CommonCrud;
using Domain.Entities;
using MediatR;

namespace Application.UseCase.OrderServices;

public sealed class GetOrderServicesPagedHandler : IRequestHandler<GetEntitiesPaged<OrderService>, PagedResult<OrderService>>
{
    private readonly IOrderServiceRepository _repository;

    public GetOrderServicesPagedHandler(IOrderServiceRepository repository) => _repository = repository;

    public async Task<PagedResult<OrderService>> Handle(GetEntitiesPaged<OrderService> request, CancellationToken ct)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize < 1 ? 10 : request.PageSize;
        var items = await _repository.GetPagedAsync(page, pageSize, request.Search, ct);
        var total = await _repository.CountAsync(request.Search, ct);

        return new PagedResult<OrderService>(items, total, page, pageSize);
    }
}

public sealed class GetOrderServiceByIdHandler : IRequestHandler<GetEntityById<OrderService>, OrderService>
{
    private readonly IOrderServiceRepository _repository;

    public GetOrderServiceByIdHandler(IOrderServiceRepository repository) => _repository = repository;

    public async Task<OrderService> Handle(GetEntityById<OrderService> request, CancellationToken ct)
    {
        return await _repository.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el servicio de la orden.");
    }
}
