// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IDocumentTypeRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;
using Domain.ValueObjects.DocumentType;

namespace Application.Abstractions;

public interface IDocumentTypeRepository
{
    Task<DocumentType?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<DocumentType?> GetByCodeAsync(DocumentTypeCode code, CancellationToken ct = default);
    Task<DocumentType?> GetByNameAsync(DocumentTypeName name, CancellationToken ct = default);
    Task<IReadOnlyList<DocumentType>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<DocumentType>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(DocumentType documentType, CancellationToken ct = default);
    Task UpdateAsync(DocumentType documentType, CancellationToken ct = default);
    Task RemoveAsync(DocumentType documentType, CancellationToken ct = default);
    Task<bool> ExistsCodeAsync(DocumentTypeCode code, CancellationToken ct = default);
    Task<bool> ExistsNameAsync(DocumentTypeName name, CancellationToken ct = default);
}
