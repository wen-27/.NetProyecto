using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.EmailDomain;
using MediatR;

namespace Application.UseCase.EmailDomains;

public sealed class CreateEmailDomainHandler : IRequestHandler<CreateEmailDomain, int>
{
    private readonly IEmailDomainRepository _emailDomains;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmailDomainHandler(IEmailDomainRepository emailDomains, IUnitOfWork unitOfWork)
    {
        _emailDomains = emailDomains;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateEmailDomain request, CancellationToken ct)
    {
        var domain = new EmailDomainValue(request.Domain);

        if (await _emailDomains.ExistsDomainAsync(domain, ct))
        {
            throw new InvalidOperationException("Ya existe ese dominio de correo.");
        }

        var emailDomain = new EmailDomain { Domain = domain.Value };

        await _emailDomains.AddAsync(emailDomain, ct);
        await _unitOfWork.CommitAsync(ct);

        return emailDomain.Id;
    }
}
