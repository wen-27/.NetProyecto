using Domain.Entities;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IPaymentRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<Payment?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Payment>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(Payment payment, CancellationToken ct = default);
    Task UpdateAsync(Payment payment, CancellationToken ct = default);
    Task RemoveAsync(Payment payment, CancellationToken ct = default);
}
