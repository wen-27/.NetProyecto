using Domain.Entities;

namespace Application.Abstractions;

public interface ICardTypeRepository
{
    Task<CardType?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<CardType>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(CardType cardType, CancellationToken ct = default);
    Task UpdateAsync(CardType cardType, CancellationToken ct = default);
    Task RemoveAsync(CardType cardType, CancellationToken ct = default);
}
