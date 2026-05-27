using Domain.Entities;
using Domain.ValueObjects.Audit;

namespace Application.Abstractions;

public interface IAuditRepository
{
    Task<Audit?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Audit>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Audit>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task<IReadOnlyList<Audit>> GetByUserIdAsync(AuditUserId userId, CancellationToken ct = default);
    Task<IReadOnlyList<Audit>> GetByAffectedEntityAsync(AuditAffectedEntity affectedEntity, CancellationToken ct = default);
    Task AddAsync(Audit audit, CancellationToken ct = default);
    Task UpdateAsync(Audit audit, CancellationToken ct = default);
    Task RemoveAsync(Audit audit, CancellationToken ct = default);
}
