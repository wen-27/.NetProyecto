// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IDepartmentRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;

namespace Application.Abstractions;

public interface IDepartmentRepository
{
    Task<Department?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Department>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(Department department, CancellationToken ct = default);
    Task UpdateAsync(Department department, CancellationToken ct = default);
    Task RemoveAsync(Department department, CancellationToken ct = default);
}
