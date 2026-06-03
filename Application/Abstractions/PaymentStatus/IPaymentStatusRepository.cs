// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IPaymentStatusRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;

namespace Application.Abstractions;

public interface IPaymentStatusRepository
{
    Task<PaymentStatus?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<PaymentStatus>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PaymentStatus status, CancellationToken ct = default);
    Task UpdateAsync(PaymentStatus status, CancellationToken ct = default);
    Task RemoveAsync(PaymentStatus status, CancellationToken ct = default);
}
