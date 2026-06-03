using Domain.Entities;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IPaymentStatusRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<PaymentStatus?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<PaymentStatus>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PaymentStatus status, CancellationToken ct = default);
    Task UpdateAsync(PaymentStatus status, CancellationToken ct = default);
    Task RemoveAsync(PaymentStatus status, CancellationToken ct = default);
}
