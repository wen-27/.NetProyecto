namespace Api.DTOs.EmailDomains;

// DTO usado para transportar datos de CreateEmailDomainRequest entre la API y sus consumidores.
public sealed record CreateEmailDomainRequest(string Domain);
// DTO usado para transportar datos de EmailDomainResponse entre la API y sus consumidores.
public sealed record EmailDomainResponse(int Id, string Domain);
