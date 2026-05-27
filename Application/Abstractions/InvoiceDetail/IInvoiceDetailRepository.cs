using Domain.Entities;
using Domain.ValueObjects.InvoiceDetail;

namespace Application.Abstractions;

public interface IInvoiceDetailRepository
{
    Task<InvoiceDetail?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<InvoiceDetail>> GetByInvoiceIdAsync(InvoiceDetailInvoiceId invoiceId, CancellationToken ct = default);
    Task<IReadOnlyList<InvoiceDetail>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<InvoiceDetail>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(InvoiceDetail invoiceDetail, CancellationToken ct = default);
    Task UpdateAsync(InvoiceDetail invoiceDetail, CancellationToken ct = default);
    Task RemoveAsync(InvoiceDetail invoiceDetail, CancellationToken ct = default);
}
