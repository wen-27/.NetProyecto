// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IInvoiceRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;
using Domain.ValueObjects.Invoice;

namespace Application.Abstractions;

public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Invoice?> GetByServiceOrderIdAsync(InvoiceServiceOrderId serviceOrderId, CancellationToken ct = default);
    Task<IReadOnlyList<Invoice>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Invoice>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(Invoice invoice, CancellationToken ct = default);
    Task UpdateAsync(Invoice invoice, CancellationToken ct = default);
    Task RemoveAsync(Invoice invoice, CancellationToken ct = default);
    Task<bool> ExistsServiceOrderIdAsync(InvoiceServiceOrderId serviceOrderId, CancellationToken ct = default);
}
