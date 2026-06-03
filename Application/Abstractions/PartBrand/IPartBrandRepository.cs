// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IPartBrandRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;

namespace Application.Abstractions;

public interface IPartBrandRepository
{
    Task<PartBrand?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<PartBrand>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PartBrand partBrand, CancellationToken ct = default);
    Task UpdateAsync(PartBrand partBrand, CancellationToken ct = default);
    Task RemoveAsync(PartBrand partBrand, CancellationToken ct = default);
}
