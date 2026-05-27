using Domain.Entities;
using Domain.ValueObjects.PersonDocument;

namespace Application.Abstractions;

public interface IPersonDocumentRepository
{
    Task<PersonDocument?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<PersonDocument?> GetByDocumentAsync(PersonDocumentDocumentTypeId documentTypeId, PersonDocumentNumber documentNumber, CancellationToken ct = default);
    Task<IReadOnlyList<PersonDocument>> GetByPersonIdAsync(PersonDocumentPersonId personId, CancellationToken ct = default);
    Task<IReadOnlyList<PersonDocument>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<PersonDocument>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PersonDocument personDocument, CancellationToken ct = default);
    Task UpdateAsync(PersonDocument personDocument, CancellationToken ct = default);
    Task RemoveAsync(PersonDocument personDocument, CancellationToken ct = default);
    Task<bool> ExistsDocumentAsync(PersonDocumentDocumentTypeId documentTypeId, PersonDocumentNumber documentNumber, CancellationToken ct = default);
}
