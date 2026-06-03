// Responsabilidad: Contrato de Application que define lo que la capa de negocio necesita de servicios externos o persistencia para IPersonEmailRepository.
// Nota de mantenimiento: Mantener este archivo cohesivo ayuda a que el backend sea mas facil de probar y evolucionar.
using Domain.Entities;
using Domain.ValueObjects.PersonEmail;

namespace Application.Abstractions;

public interface IPersonEmailRepository
{
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
