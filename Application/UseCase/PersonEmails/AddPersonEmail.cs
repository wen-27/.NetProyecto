using MediatR;

namespace Application.UseCase.PersonEmails;

// Caso de uso que modela una accion o consulta de negocio relacionada con AddPersonEmail.
public sealed record AddPersonEmail(int PersonId, int EmailDomainId, string EmailUser, bool IsPrimary) : IRequest<int>;
