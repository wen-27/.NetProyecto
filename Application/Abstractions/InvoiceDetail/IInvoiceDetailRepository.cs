// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IInvoiceDetailRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
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
