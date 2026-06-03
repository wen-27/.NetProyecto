using Domain.Common;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(TEntity entity, CancellationToken ct = default);
    Task UpdateAsync(TEntity entity, CancellationToken ct = default);
    Task RemoveAsync(TEntity entity, CancellationToken ct = default);
}
