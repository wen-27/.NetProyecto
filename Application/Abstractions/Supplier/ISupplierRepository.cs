using Domain.Entities;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface ISupplierRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<Supplier?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Supplier>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(Supplier supplier, CancellationToken ct = default);
    Task UpdateAsync(Supplier supplier, CancellationToken ct = default);
    Task RemoveAsync(Supplier supplier, CancellationToken ct = default);
}
