// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IPaymentCardRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
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
