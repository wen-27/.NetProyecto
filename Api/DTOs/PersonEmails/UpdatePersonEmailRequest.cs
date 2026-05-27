namespace Api.DTOs.PersonEmails;

public sealed record UpdatePersonEmailRequest(int EmailDomainId, string EmailUser, bool IsPrimary);
