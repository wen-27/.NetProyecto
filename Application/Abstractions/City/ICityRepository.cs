using Domain.Entities;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface ICityRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<City?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<City>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(City city, CancellationToken ct = default);
    Task UpdateAsync(City city, CancellationToken ct = default);
    Task RemoveAsync(City city, CancellationToken ct = default);
}
