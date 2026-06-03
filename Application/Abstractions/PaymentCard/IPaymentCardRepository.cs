using Domain.Entities;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IPaymentCardRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<PaymentCard?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<PaymentCard>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PaymentCard card, CancellationToken ct = default);
    Task UpdateAsync(PaymentCard card, CancellationToken ct = default);
    Task RemoveAsync(PaymentCard card, CancellationToken ct = default);
}
