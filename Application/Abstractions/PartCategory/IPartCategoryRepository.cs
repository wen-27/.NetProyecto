using Domain.Entities;
using Domain.ValueObjects.PartCategory;

namespace Application.Abstractions;

public interface IPartCategoryRepository
{
    Task<PartCategory?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<PartCategory?> GetByNameAsync(PartCategoryName name, CancellationToken ct = default);
    Task<IReadOnlyList<PartCategory>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<PartCategory>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PartCategory partCategory, CancellationToken ct = default);
    Task UpdateAsync(PartCategory partCategory, CancellationToken ct = default);
    Task RemoveAsync(PartCategory partCategory, CancellationToken ct = default);
    Task<bool> ExistsNameAsync(PartCategoryName name, CancellationToken ct = default);
}
