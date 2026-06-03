// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IOrderServiceRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;

namespace Application.Abstractions;

public interface IOrderServiceRepository
{
    Task<OrderService?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<OrderService>> GetByServiceOrderIdAsync(int serviceOrderId, CancellationToken ct = default);
    Task<IReadOnlyList<OrderService>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(OrderService orderService, CancellationToken ct = default);
    Task UpdateAsync(OrderService orderService, CancellationToken ct = default);
    Task RemoveAsync(OrderService orderService, CancellationToken ct = default);
    Task<bool> ExistsServiceOrderAndServiceTypeAsync(int serviceOrderId, int serviceTypeId, CancellationToken ct = default);
}
