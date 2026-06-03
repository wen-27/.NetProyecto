using Domain.Entities;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IPersonRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<Person?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Person>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Person>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task<bool> HasActiveServiceOrdersAsCurrentOwnerAsync(int personId, CancellationToken ct = default);
    Task<bool> HasActiveServiceOrdersAsMechanicAsync(int personId, CancellationToken ct = default);
    Task AddAsync(Person person, CancellationToken ct = default);
    Task UpdateAsync(Person person, CancellationToken ct = default);
    Task RemoveAsync(Person person, CancellationToken ct = default);
}
