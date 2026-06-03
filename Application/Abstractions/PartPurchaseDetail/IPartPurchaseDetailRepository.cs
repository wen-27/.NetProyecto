// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IPartPurchaseDetailRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;

namespace Application.Abstractions;

public interface IPartPurchaseDetailRepository
{
    Task<PartPurchaseDetail?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<PartPurchaseDetail>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PartPurchaseDetail detail, CancellationToken ct = default);
    Task UpdateAsync(PartPurchaseDetail detail, CancellationToken ct = default);
    Task RemoveAsync(PartPurchaseDetail detail, CancellationToken ct = default);
}
