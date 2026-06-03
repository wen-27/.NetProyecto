// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con UpdateEmailDomainHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.ValueObjects.EmailDomain;
using MediatR;

namespace Application.UseCase.EmailDomains;

public sealed class UpdateEmailDomainHandler : IRequestHandler<UpdateEmailDomain>
{
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
