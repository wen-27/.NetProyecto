// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para ISupplierRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;

namespace Application.Abstractions;

public interface ISupplierRepository
{
    Task<Supplier?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Supplier>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(Supplier supplier, CancellationToken ct = default);
    Task UpdateAsync(Supplier supplier, CancellationToken ct = default);
    Task RemoveAsync(Supplier supplier, CancellationToken ct = default);
}
