using Domain.Entities;
using Domain.ValueObjects.PersonPhone;

namespace Application.Abstractions;

public interface IPersonPhoneRepository
{
    Task<PersonPhone?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<PersonPhone?> GetByPhoneAsync(PersonPhoneCountryId countryId, PersonPhoneNumber phoneNumber, CancellationToken ct = default);
    Task<IReadOnlyList<PersonPhone>> GetByPersonIdAsync(PersonPhonePersonId personId, CancellationToken ct = default);
    Task<IReadOnlyList<PersonPhone>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<PersonPhone>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PersonPhone personPhone, CancellationToken ct = default);
    Task UpdateAsync(PersonPhone personPhone, CancellationToken ct = default);
    Task RemoveAsync(PersonPhone personPhone, CancellationToken ct = default);
    Task<bool> ExistsPhoneAsync(PersonPhoneCountryId countryId, PersonPhoneNumber phoneNumber, CancellationToken ct = default);
}
