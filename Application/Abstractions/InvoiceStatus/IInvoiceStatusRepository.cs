// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IInvoiceStatusRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
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
