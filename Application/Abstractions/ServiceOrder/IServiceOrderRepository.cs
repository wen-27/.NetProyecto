using Domain.Entities;
using Domain.ValueObjects.ServiceOrder;

namespace Application.Abstractions;

public interface IServiceOrderRepository
{
    Task<ServiceOrder?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<ServiceOrder>> GetByVehicleIdAsync(ServiceOrderVehicleId vehicleId, CancellationToken ct = default);
    Task<IReadOnlyList<ServiceOrder>> GetByStatusIdAsync(ServiceOrderStatusId statusId, CancellationToken ct = default);
    Task<bool> HasActiveOrderForVehicleAsync(ServiceOrderVehicleId vehicleId, CancellationToken ct = default);
    Task<IReadOnlyList<ServiceOrder>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<ServiceOrder>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(ServiceOrder serviceOrder, CancellationToken ct = default);
    Task UpdateAsync(ServiceOrder serviceOrder, CancellationToken ct = default);
    Task RemoveAsync(ServiceOrder serviceOrder, CancellationToken ct = default);
}
