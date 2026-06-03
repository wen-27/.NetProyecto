using Domain.Entities;
using Domain.ValueObjects.Invoice;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IInvoiceRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
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
