using MediatR;

namespace Application.UseCase.PersonEmails;

public sealed record UpdatePersonEmail(int Id, int EmailDomainId, string EmailUser, bool IsPrimary) : IRequest;
