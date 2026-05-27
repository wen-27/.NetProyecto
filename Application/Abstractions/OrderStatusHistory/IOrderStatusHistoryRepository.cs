using Domain.Entities;

namespace Application.Abstractions;

public interface IOrderStatusHistoryRepository
{
    Task<OrderStatusHistory?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<OrderStatusHistory>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(OrderStatusHistory history, CancellationToken ct = default);
}
