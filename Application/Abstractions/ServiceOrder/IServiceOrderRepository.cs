// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IServiceOrderRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
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
    Task<IReadOnlyList<ServiceOrder>> GetFilteredAsync(
        int page,
        int pageSize,
        string? search = null,
        int? clientPersonId = null,
        string? vin = null,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        int? statusId = null,
        int? mechanicPersonId = null,
        CancellationToken ct = default);
    Task<int> CountFilteredAsync(
        string? search = null,
        int? clientPersonId = null,
        string? vin = null,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        int? statusId = null,
        int? mechanicPersonId = null,
        CancellationToken ct = default);
    Task AddAsync(ServiceOrder serviceOrder, CancellationToken ct = default);
    Task UpdateAsync(ServiceOrder serviceOrder, CancellationToken ct = default);
    Task RemoveAsync(ServiceOrder serviceOrder, CancellationToken ct = default);
}
