using MediatR;

namespace Application.UseCase.PersonEmails;

public sealed record AddPersonEmail(int PersonId, int EmailDomainId, string EmailUser, bool IsPrimary) : IRequest<int>;
