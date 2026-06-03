using Domain.Entities;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IDepartmentRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<Department?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Department>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(Department department, CancellationToken ct = default);
    Task UpdateAsync(Department department, CancellationToken ct = default);
    Task RemoveAsync(Department department, CancellationToken ct = default);
}
