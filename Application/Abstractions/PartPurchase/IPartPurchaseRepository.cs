// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IPartPurchaseRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;

namespace Application.Abstractions;

public interface IPartPurchaseRepository
{
    Task<PartPurchase?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<PartPurchase>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PartPurchase purchase, CancellationToken ct = default);
    Task UpdateAsync(PartPurchase purchase, CancellationToken ct = default);
    Task RemoveAsync(PartPurchase purchase, CancellationToken ct = default);
}
