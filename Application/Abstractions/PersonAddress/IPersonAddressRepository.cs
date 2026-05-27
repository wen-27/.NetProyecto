using Domain.Entities;

namespace Application.Abstractions;

public interface IPersonAddressRepository
{
    Task<PersonAddress?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<PersonAddress>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PersonAddress address, CancellationToken ct = default);
    Task UpdateAsync(PersonAddress address, CancellationToken ct = default);
    Task RemoveAsync(PersonAddress address, CancellationToken ct = default);
}
