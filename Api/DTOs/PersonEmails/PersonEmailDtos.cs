namespace Api.DTOs.PersonEmails;

public sealed record CreatePersonEmailRequest(int PersonId, int EmailDomainId, string EmailUser, bool IsPrimary);
public sealed record PersonEmailResponse(int Id, int PersonId, int EmailDomainId, string EmailUser, bool IsPrimary);
