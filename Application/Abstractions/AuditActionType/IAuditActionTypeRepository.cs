using Domain.Entities;
using Domain.ValueObjects.AuditActionType;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IAuditActionTypeRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<AuditActionType?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<AuditActionType?> GetByNameAsync(AuditActionTypeName name, CancellationToken ct = default);
    Task<IReadOnlyList<AuditActionType>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<AuditActionType>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(AuditActionType auditActionType, CancellationToken ct = default);
    Task UpdateAsync(AuditActionType auditActionType, CancellationToken ct = default);
    Task RemoveAsync(AuditActionType auditActionType, CancellationToken ct = default);
    Task<bool> ExistsNameAsync(AuditActionTypeName name, CancellationToken ct = default);
}
