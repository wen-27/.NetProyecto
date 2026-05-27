using Domain.Entities;
using Domain.ValueObjects.Customer;

namespace Application.Abstractions;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Customer?> GetByPersonIdAsync(CustomerPersonId personId, CancellationToken ct = default);
    Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Customer>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(Customer customer, CancellationToken ct = default);
    Task UpdateAsync(Customer customer, CancellationToken ct = default);
    Task RemoveAsync(Customer customer, CancellationToken ct = default);
    Task<bool> ExistsPersonIdAsync(CustomerPersonId personId, CancellationToken ct = default);
}
