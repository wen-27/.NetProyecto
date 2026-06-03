using Domain.Entities;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IPaymentMethodRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<PaymentMethod?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<PaymentMethod>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PaymentMethod method, CancellationToken ct = default);
    Task UpdateAsync(PaymentMethod method, CancellationToken ct = default);
    Task RemoveAsync(PaymentMethod method, CancellationToken ct = default);
}
