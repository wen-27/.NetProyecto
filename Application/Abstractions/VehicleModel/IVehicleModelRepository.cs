using Domain.Entities;
using Domain.ValueObjects.VehicleModel;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IVehicleModelRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<VehicleModel?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<VehicleModel?> GetByBrandAndNameAsync(VehicleModelBrandId brandId, VehicleModelName name, CancellationToken ct = default);
    Task<IReadOnlyList<VehicleModel>> GetByBrandIdAsync(VehicleModelBrandId brandId, CancellationToken ct = default);
    Task<IReadOnlyList<VehicleModel>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<VehicleModel>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(VehicleModel vehicleModel, CancellationToken ct = default);
    Task UpdateAsync(VehicleModel vehicleModel, CancellationToken ct = default);
    Task RemoveAsync(VehicleModel vehicleModel, CancellationToken ct = default);
    Task<bool> ExistsBrandAndNameAsync(VehicleModelBrandId brandId, VehicleModelName name, CancellationToken ct = default);
}
