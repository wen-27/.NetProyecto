using Domain.Entities;
using Domain.ValueObjects.UserRole;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IUserRoleRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<UserRole?> GetByIdsAsync(UserRoleUserId userId, UserRoleRoleId roleId, CancellationToken ct = default);
    Task<IReadOnlyList<UserRole>> GetByUserIdAsync(UserRoleUserId userId, CancellationToken ct = default);
    Task<IReadOnlyList<UserRole>> GetByRoleIdAsync(UserRoleRoleId roleId, CancellationToken ct = default);
    Task<IReadOnlyList<UserRole>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<UserRole>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(UserRole userRole, CancellationToken ct = default);
    Task UpdateAsync(UserRole userRole, CancellationToken ct = default);
    Task RemoveAsync(UserRole userRole, CancellationToken ct = default);
    Task<bool> ExistsAsync(UserRoleUserId userId, UserRoleRoleId roleId, CancellationToken ct = default);
}
