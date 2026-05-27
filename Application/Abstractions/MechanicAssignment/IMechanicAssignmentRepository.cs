using Domain.Entities;

namespace Application.Abstractions;

public interface IMechanicAssignmentRepository
{
    Task<MechanicAssignment?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<MechanicAssignment>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(MechanicAssignment assignment, CancellationToken ct = default);
    Task UpdateAsync(MechanicAssignment assignment, CancellationToken ct = default);
    Task RemoveAsync(MechanicAssignment assignment, CancellationToken ct = default);
    Task<bool> HasActiveAssignmentAsync(int mechanicPersonId, int orderServiceId, CancellationToken ct = default);
}
