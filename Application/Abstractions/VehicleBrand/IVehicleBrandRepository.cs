using Domain.Entities;
using Domain.ValueObjects.VehicleBrand;

namespace Application.Abstractions;

public interface IVehicleBrandRepository
{
    Task<VehicleBrand?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<VehicleBrand?> GetByNameAsync(VehicleBrandName name, CancellationToken ct = default);
    Task<IReadOnlyList<VehicleBrand>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<VehicleBrand>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(VehicleBrand vehicleBrand, CancellationToken ct = default);
    Task UpdateAsync(VehicleBrand vehicleBrand, CancellationToken ct = default);
    Task RemoveAsync(VehicleBrand vehicleBrand, CancellationToken ct = default);
    Task<bool> ExistsNameAsync(VehicleBrandName name, CancellationToken ct = default);
}
