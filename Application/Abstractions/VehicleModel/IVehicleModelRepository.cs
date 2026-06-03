// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IVehicleModelRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;
using Domain.ValueObjects.VehicleModel;

namespace Application.Abstractions;

public interface IVehicleModelRepository
{
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
