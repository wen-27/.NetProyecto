// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IVehicleRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;
using Domain.ValueObjects.Vehicle;

namespace Application.Abstractions;

public interface IVehicleRepository
{
    Task<Vehicle?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Vehicle?> GetByPlateAsync(VehiclePlate plate, CancellationToken ct = default);
    Task<Vehicle?> GetByVinAsync(VehicleVin vin, CancellationToken ct = default);
    Task<IReadOnlyList<Vehicle>> GetByModelIdAsync(VehicleModelId modelId, CancellationToken ct = default);
    Task<IReadOnlyList<Vehicle>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Vehicle>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task<IReadOnlyList<Vehicle>> GetFilteredAsync(
        int page,
        int pageSize,
        string? search = null,
        string? vin = null,
        int? clientPersonId = null,
        CancellationToken ct = default);
    Task<int> CountFilteredAsync(
        string? search = null,
        string? vin = null,
        int? clientPersonId = null,
        CancellationToken ct = default);
    Task AddAsync(Vehicle vehicle, CancellationToken ct = default);
    Task UpdateAsync(Vehicle vehicle, CancellationToken ct = default);
    Task RemoveAsync(Vehicle vehicle, CancellationToken ct = default);
    Task<bool> ExistsPlateAsync(VehiclePlate plate, CancellationToken ct = default);
    Task<bool> ExistsVinAsync(VehicleVin vin, CancellationToken ct = default);
}
