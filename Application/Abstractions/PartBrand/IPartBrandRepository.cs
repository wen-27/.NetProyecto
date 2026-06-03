using Domain.Entities;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IPartBrandRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<PartBrand?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<PartBrand>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PartBrand partBrand, CancellationToken ct = default);
    Task UpdateAsync(PartBrand partBrand, CancellationToken ct = default);
    Task RemoveAsync(PartBrand partBrand, CancellationToken ct = default);
}
