using Domain.Entities;
using Domain.ValueObjects.User;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IUserRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<User?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<User?> GetByPersonIdAsync(UserPersonId personId, CancellationToken ct = default);
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<User>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(User user, CancellationToken ct = default);
    Task UpdateAsync(User user, CancellationToken ct = default);
    Task RemoveAsync(User user, CancellationToken ct = default);
    Task<bool> ExistsPersonIdAsync(UserPersonId personId, CancellationToken ct = default);
}
