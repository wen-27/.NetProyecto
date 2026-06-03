using MediatR;

namespace Application.UseCase.EmailDomains;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateEmailDomain.
public sealed record CreateEmailDomain(string Domain) : IRequest<int>;
