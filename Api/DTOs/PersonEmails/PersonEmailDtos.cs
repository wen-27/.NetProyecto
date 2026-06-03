namespace Api.DTOs.PersonEmails;

// DTO usado para transportar datos de CreatePersonEmailRequest entre la API y sus consumidores.
public sealed record CreatePersonEmailRequest(int PersonId, int EmailDomainId, string EmailUser, bool IsPrimary);
// DTO usado para transportar datos de PersonEmailResponse entre la API y sus consumidores.
public sealed record PersonEmailResponse(int Id, int PersonId, int EmailDomainId, string EmailUser, bool IsPrimary);
