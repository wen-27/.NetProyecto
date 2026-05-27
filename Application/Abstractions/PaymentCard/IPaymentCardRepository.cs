using Domain.Entities;

namespace Application.Abstractions;

public interface IPaymentCardRepository
{
    Task<PaymentCard?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<PaymentCard>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PaymentCard card, CancellationToken ct = default);
    Task UpdateAsync(PaymentCard card, CancellationToken ct = default);
    Task RemoveAsync(PaymentCard card, CancellationToken ct = default);
}
