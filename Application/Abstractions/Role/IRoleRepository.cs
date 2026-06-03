using Domain.Entities;
using Domain.ValueObjects.Role;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IRoleRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<Role?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Role?> GetByNameAsync(RoleName name, CancellationToken ct = default);
    Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Role>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(Role role, CancellationToken ct = default);
    Task UpdateAsync(Role role, CancellationToken ct = default);
    Task RemoveAsync(Role role, CancellationToken ct = default);
    Task<bool> ExistsNameAsync(RoleName name, CancellationToken ct = default);
}
