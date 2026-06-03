using Domain.Entities;
using Domain.ValueObjects.PersonEmail;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IPersonEmailRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<PersonEmail?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<PersonEmail?> GetByEmailAsync(PersonEmailUser emailUser, PersonEmailDomainId emailDomainId, CancellationToken ct = default);
    Task<IReadOnlyList<PersonEmail>> GetByPersonIdAsync(PersonEmailPersonId personId, CancellationToken ct = default);
    Task<IReadOnlyList<PersonEmail>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<PersonEmail>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(PersonEmail personEmail, CancellationToken ct = default);
    Task UpdateAsync(PersonEmail personEmail, CancellationToken ct = default);
    Task RemoveAsync(PersonEmail personEmail, CancellationToken ct = default);
    Task<bool> ExistsEmailAsync(PersonEmailUser emailUser, PersonEmailDomainId emailDomainId, CancellationToken ct = default);
}
