using MediatR;

namespace Application.UseCase.EmailDomains;

public sealed record CreateEmailDomain(string Domain) : IRequest<int>;
