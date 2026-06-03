using MediatR;

namespace Application.UseCase.EmailDomains;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateEmailDomain.
public sealed record UpdateEmailDomain(int Id, string Domain) : IRequest;
