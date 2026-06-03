// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para ICityRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;

namespace Application.Abstractions;

public interface ICityRepository
{
    Task<City?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<City>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(City city, CancellationToken ct = default);
    Task UpdateAsync(City city, CancellationToken ct = default);
    Task RemoveAsync(City city, CancellationToken ct = default);
}
