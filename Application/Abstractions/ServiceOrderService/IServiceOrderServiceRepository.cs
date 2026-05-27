using Domain.Entities;

namespace Application.Abstractions;

public interface IServiceOrderServiceRepository
{
    Task<ServiceOrderService?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<ServiceOrderService?> GetByServiceOrderAndServiceTypeAsync(int serviceOrderId, int serviceTypeId, CancellationToken ct = default);
    Task<IReadOnlyList<ServiceOrderService>> GetByServiceOrderIdAsync(int serviceOrderId, CancellationToken ct = default);
    Task<IReadOnlyList<ServiceOrderService>> GetByMechanicIdAsync(int mechanicId, CancellationToken ct = default);
    Task<IReadOnlyList<ServiceOrderService>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task<bool> ExistsServiceOrderAndServiceTypeAsync(int serviceOrderId, int serviceTypeId, CancellationToken ct = default);
    Task AddAsync(ServiceOrderService service, CancellationToken ct = default);
    Task UpdateAsync(ServiceOrderService service, CancellationToken ct = default);
    Task RemoveAsync(ServiceOrderService service, CancellationToken ct = default);
}
