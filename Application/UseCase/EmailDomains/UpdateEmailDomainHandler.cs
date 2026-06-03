using Application.Abstractions;
using Domain.ValueObjects.EmailDomain;
using MediatR;

namespace Application.UseCase.EmailDomains;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateEmailDomain.
public sealed class UpdateEmailDomainHandler : IRequestHandler<UpdateEmailDomain>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IEmailDomainRepository _emailDomains;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmailDomainHandler(IEmailDomainRepository emailDomains, IUnitOfWork unitOfWork)
    {
        _emailDomains = emailDomains;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateEmailDomain request, CancellationToken ct)
    {
        var emailDomain = await _emailDomains.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el dominio de correo.");

        var domain = new EmailDomainValue(request.Domain);
        emailDomain.Domain = domain.Value;

        await _emailDomains.UpdateAsync(emailDomain, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
