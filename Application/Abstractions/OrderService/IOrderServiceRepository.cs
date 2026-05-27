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
