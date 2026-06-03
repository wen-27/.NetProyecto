namespace Api.DTOs.PersonEmails;

// DTO usado para transportar datos de UpdatePersonEmailRequest entre la API y sus consumidores.
public sealed record UpdatePersonEmailRequest(int EmailDomainId, string EmailUser, bool IsPrimary);
