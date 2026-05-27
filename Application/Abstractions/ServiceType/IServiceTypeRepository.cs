using Domain.Entities;
using Domain.ValueObjects.ServiceType;

namespace Application.Abstractions;

public interface IServiceTypeRepository
{
    Task<ServiceType?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<ServiceType?> GetByNameAsync(ServiceTypeName name, CancellationToken ct = default);
    Task<IReadOnlyList<ServiceType>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<ServiceType>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(ServiceType serviceType, CancellationToken ct = default);
    Task UpdateAsync(ServiceType serviceType, CancellationToken ct = default);
    Task RemoveAsync(ServiceType serviceType, CancellationToken ct = default);
    Task<bool> ExistsNameAsync(ServiceTypeName name, CancellationToken ct = default);
}
