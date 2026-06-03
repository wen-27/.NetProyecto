using Domain.Entities;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IOrderStatusHistoryRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<OrderStatusHistory?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<OrderStatusHistory>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(OrderStatusHistory history, CancellationToken ct = default);
}
