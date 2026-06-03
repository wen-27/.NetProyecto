// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IOrderStatusRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;
using Domain.ValueObjects.OrderStatus;

namespace Application.Abstractions;

public interface IOrderStatusRepository
{
    Task<OrderStatus?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<OrderStatus?> GetByNameAsync(OrderStatusName name, CancellationToken ct = default);
    Task<IReadOnlyList<OrderStatus>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<OrderStatus>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(OrderStatus orderStatus, CancellationToken ct = default);
    Task UpdateAsync(OrderStatus orderStatus, CancellationToken ct = default);
    Task RemoveAsync(OrderStatus orderStatus, CancellationToken ct = default);
    Task<bool> ExistsNameAsync(OrderStatusName name, CancellationToken ct = default);
}
