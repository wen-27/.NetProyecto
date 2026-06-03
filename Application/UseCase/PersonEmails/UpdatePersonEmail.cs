using MediatR;

namespace Application.UseCase.PersonEmails;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdatePersonEmail.
public sealed record UpdatePersonEmail(int Id, int EmailDomainId, string EmailUser, bool IsPrimary) : IRequest;
