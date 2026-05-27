using Domain.Entities;

namespace Application.Abstractions;

public interface IPartPurchaseRepository
{
    Task<PartPurchase?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<PartPurchase>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PartPurchase purchase, CancellationToken ct = default);
    Task UpdateAsync(PartPurchase purchase, CancellationToken ct = default);
    Task RemoveAsync(PartPurchase purchase, CancellationToken ct = default);
}
