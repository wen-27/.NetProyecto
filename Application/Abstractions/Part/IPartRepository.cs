using Domain.Entities;
using Domain.ValueObjects.Part;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IPartRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<Part?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Part?> GetByCodeAsync(PartCode code, CancellationToken ct = default);
    Task<IReadOnlyList<Part>> GetByCategoryIdAsync(PartCategoryId categoryId, CancellationToken ct = default);
    Task<IReadOnlyList<Part>> GetLowStockAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Part>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Part>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(Part part, CancellationToken ct = default);
    Task UpdateAsync(Part part, CancellationToken ct = default);
    Task RemoveAsync(Part part, CancellationToken ct = default);
    Task<bool> ExistsCodeAsync(PartCode code, CancellationToken ct = default);
}
