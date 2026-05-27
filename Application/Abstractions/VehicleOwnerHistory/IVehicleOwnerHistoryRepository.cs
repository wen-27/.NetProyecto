using Domain.Entities;
using Domain.ValueObjects.VehicleOwnerHistory;

namespace Application.Abstractions;

public interface IVehicleOwnerHistoryRepository
{
    Task<VehicleOwnerHistory?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<VehicleOwnerHistory>> GetByVehicleIdAsync(VehicleOwnerHistoryVehicleId vehicleId, CancellationToken ct = default);
    Task<IReadOnlyList<VehicleOwnerHistory>> GetByCustomerIdAsync(VehicleOwnerHistoryCustomerId customerId, CancellationToken ct = default);
    Task<VehicleOwnerHistory?> GetCurrentByVehicleIdAsync(VehicleOwnerHistoryVehicleId vehicleId, CancellationToken ct = default);
    Task<IReadOnlyList<VehicleOwnerHistory>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<VehicleOwnerHistory>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(VehicleOwnerHistory vehicleOwnerHistory, CancellationToken ct = default);
    Task UpdateAsync(VehicleOwnerHistory vehicleOwnerHistory, CancellationToken ct = default);
    Task RemoveAsync(VehicleOwnerHistory vehicleOwnerHistory, CancellationToken ct = default);
}
