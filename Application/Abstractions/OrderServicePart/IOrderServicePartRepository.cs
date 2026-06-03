// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IOrderServicePartRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;

namespace Application.Abstractions;

public interface IOrderServicePartRepository
{
    Task<OrderServicePart?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<OrderServicePart>> GetByOrderServiceIdAsync(int orderServiceId, CancellationToken ct = default);
    Task<IReadOnlyList<OrderServicePart>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(OrderServicePart orderServicePart, CancellationToken ct = default);
    Task UpdateAsync(OrderServicePart orderServicePart, CancellationToken ct = default);
    Task RemoveAsync(OrderServicePart orderServicePart, CancellationToken ct = default);
    Task<bool> ExistsOrderServiceAndPartAsync(int orderServiceId, int partId, CancellationToken ct = default);
}
