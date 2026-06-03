// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IRoleRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;
using Domain.ValueObjects.Role;

namespace Application.Abstractions;

public interface IRoleRepository
{
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
