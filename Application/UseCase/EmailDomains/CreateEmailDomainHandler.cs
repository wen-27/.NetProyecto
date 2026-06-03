using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.EmailDomain;
using MediatR;

namespace Application.UseCase.EmailDomains;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateEmailDomain.
public sealed class CreateEmailDomainHandler : IRequestHandler<CreateEmailDomain, int>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
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
