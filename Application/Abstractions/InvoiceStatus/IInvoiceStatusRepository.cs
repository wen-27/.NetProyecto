using Domain.Entities;

namespace Application.Abstractions;

public interface IInvoiceStatusRepository
{
    Task<InvoiceStatus?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<InvoiceStatus>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(InvoiceStatus status, CancellationToken ct = default);
    Task UpdateAsync(InvoiceStatus status, CancellationToken ct = default);
    Task RemoveAsync(InvoiceStatus status, CancellationToken ct = default);
}
