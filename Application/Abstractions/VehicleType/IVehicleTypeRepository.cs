using Domain.Entities;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IVehicleTypeRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<VehicleType?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<VehicleType>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(VehicleType vehicleType, CancellationToken ct = default);
    Task UpdateAsync(VehicleType vehicleType, CancellationToken ct = default);
    Task RemoveAsync(VehicleType vehicleType, CancellationToken ct = default);
}
