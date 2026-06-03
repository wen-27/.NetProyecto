namespace Api.DTOs.EmailDomains;

// DTO usado para transportar datos de UpdateEmailDomainRequest entre la API y sus consumidores.
public sealed record UpdateEmailDomainRequest(string Domain);
