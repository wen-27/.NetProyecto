using Domain.Entities;
using Domain.ValueObjects.InvoiceDetail;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IInvoiceDetailRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<InvoiceDetail?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<InvoiceDetail>> GetByInvoiceIdAsync(InvoiceDetailInvoiceId invoiceId, CancellationToken ct = default);
    Task<IReadOnlyList<InvoiceDetail>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<InvoiceDetail>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(InvoiceDetail invoiceDetail, CancellationToken ct = default);
    Task UpdateAsync(InvoiceDetail invoiceDetail, CancellationToken ct = default);
    Task RemoveAsync(InvoiceDetail invoiceDetail, CancellationToken ct = default);
}
