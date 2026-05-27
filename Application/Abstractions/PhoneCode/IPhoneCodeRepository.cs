using Domain.Entities;
using Domain.ValueObjects.PhoneCode;

namespace Application.Abstractions;

public interface IPhoneCodeRepository
{
    Task<PhoneCode?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<PhoneCode?> GetByCodeAsync(PhoneCodeCode code, CancellationToken ct = default);
    Task<IReadOnlyList<PhoneCode>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<PhoneCode>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PhoneCode phoneCode, CancellationToken ct = default);
    Task UpdateAsync(PhoneCode phoneCode, CancellationToken ct = default);
    Task RemoveAsync(PhoneCode phoneCode, CancellationToken ct = default);
    Task<bool> ExistsCodeAsync(PhoneCodeCode code, CancellationToken ct = default);
}
