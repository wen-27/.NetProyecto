using Domain.Entities;
using Domain.ValueObjects.EmailDomain;

namespace Application.Abstractions;

// Contrato que Application usa para depender de una capacidad sin conocer su implementacion.
public interface IEmailDomainRepository
{
    // Las firmas declaradas aqui permiten intercambiar implementaciones sin cambiar los casos de uso que las consumen.
    Task<EmailDomain?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<EmailDomain?> GetByDomainAsync(EmailDomainValue domain, CancellationToken ct = default);
    Task<IReadOnlyList<EmailDomain>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<EmailDomain>> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default);
    Task<int> CountAsync(string? search = null, CancellationToken ct = default);
    Task AddAsync(EmailDomain emailDomain, CancellationToken ct = default);
    Task UpdateAsync(EmailDomain emailDomain, CancellationToken ct = default);
    Task RemoveAsync(EmailDomain emailDomain, CancellationToken ct = default);
    Task<bool> ExistsDomainAsync(EmailDomainValue domain, CancellationToken ct = default);
}
