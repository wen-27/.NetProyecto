using Domain.Entities;
using Domain.ValueObjects.OrderPartDetail;

namespace Application.Abstractions;

public interface IOrderPartDetailRepository
{
    Task<OrderPartDetail?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<OrderPartDetail?> GetByServiceOrderAndPartAsync(OrderPartDetailServiceOrderId serviceOrderId, OrderPartDetailPartId partId, CancellationToken ct = default);
    Task<IReadOnlyList<OrderPartDetail>> GetByServiceOrderIdAsync(OrderPartDetailServiceOrderId serviceOrderId, CancellationToken ct = default);
    Task<IReadOnlyList<OrderPartDetail>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<OrderPartDetail>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(OrderPartDetail orderPartDetail, CancellationToken ct = default);
    Task UpdateAsync(OrderPartDetail orderPartDetail, CancellationToken ct = default);
    Task RemoveAsync(OrderPartDetail orderPartDetail, CancellationToken ct = default);
    Task<bool> ExistsServiceOrderAndPartAsync(OrderPartDetailServiceOrderId serviceOrderId, OrderPartDetailPartId partId, CancellationToken ct = default);
}
