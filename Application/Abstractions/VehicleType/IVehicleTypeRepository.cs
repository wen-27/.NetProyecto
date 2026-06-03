// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IVehicleTypeRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;

namespace Application.Abstractions;

public interface IVehicleTypeRepository
{
    Task<VehicleType?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<VehicleType>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(VehicleType vehicleType, CancellationToken ct = default);
    Task UpdateAsync(VehicleType vehicleType, CancellationToken ct = default);
    Task RemoveAsync(VehicleType vehicleType, CancellationToken ct = default);
}
