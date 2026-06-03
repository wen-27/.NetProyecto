// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IUserRoleRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;
using Domain.ValueObjects.UserRole;

namespace Application.Abstractions;

public interface IUserRoleRepository
{
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
