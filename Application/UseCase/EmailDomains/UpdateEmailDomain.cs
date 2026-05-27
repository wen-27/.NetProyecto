using MediatR;

namespace Application.UseCase.EmailDomains;

public sealed record UpdateEmailDomain(int Id, string Domain) : IRequest;
