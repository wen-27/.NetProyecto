using Domain.Entities;
using Domain.ValueObjects.Vehicle;

namespace Application.Abstractions;

public interface IVehicleRepository
{
    Task<Vehicle?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Vehicle?> GetByVinAsync(VehicleVin vin, CancellationToken ct = default);
    Task<IReadOnlyList<Vehicle>> GetByModelIdAsync(VehicleModelId modelId, CancellationToken ct = default);
    Task<IReadOnlyList<Vehicle>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Vehicle>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(Vehicle vehicle, CancellationToken ct = default);
    Task UpdateAsync(Vehicle vehicle, CancellationToken ct = default);
    Task RemoveAsync(Vehicle vehicle, CancellationToken ct = default);
    Task<bool> ExistsVinAsync(VehicleVin vin, CancellationToken ct = default);
}
