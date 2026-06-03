using Domain.Entities;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IOrderServicePartRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<OrderServicePart?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<OrderServicePart>> GetByOrderServiceIdAsync(int orderServiceId, CancellationToken ct = default);
    Task<IReadOnlyList<OrderServicePart>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(OrderServicePart orderServicePart, CancellationToken ct = default);
    Task UpdateAsync(OrderServicePart orderServicePart, CancellationToken ct = default);
    Task RemoveAsync(OrderServicePart orderServicePart, CancellationToken ct = default);
    Task<bool> ExistsOrderServiceAndPartAsync(int orderServiceId, int partId, CancellationToken ct = default);
}
