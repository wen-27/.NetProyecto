using Domain.Entities;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface ICardTypeRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<CardType?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<CardType>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(CardType cardType, CancellationToken ct = default);
    Task UpdateAsync(CardType cardType, CancellationToken ct = default);
    Task RemoveAsync(CardType cardType, CancellationToken ct = default);
}
