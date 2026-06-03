// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IAuditActionTypeRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;
using Domain.ValueObjects.AuditActionType;

namespace Application.Abstractions;

public interface IAuditActionTypeRepository
{
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
