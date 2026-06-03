using Application.Abstractions;
using Domain.ValueObjects.PersonEmail;
using MediatR;

namespace Application.UseCase.PersonEmails;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdatePersonEmail.
public sealed class UpdatePersonEmailHandler : IRequestHandler<UpdatePersonEmail>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPersonEmailRepository _personEmails;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePersonEmailHandler(IPersonEmailRepository personEmails, IUnitOfWork unitOfWork)
    {
        _personEmails = personEmails;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdatePersonEmail request, CancellationToken ct)
    {
        var personEmail = await _personEmails.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el correo de la persona.");

        var emailDomainId = new PersonEmailDomainId(request.EmailDomainId);
        var emailUser = new PersonEmailUser(request.EmailUser);
        var isPrimary = new PersonEmailIsPrimary(request.IsPrimary);

        personEmail.EmailDomainId = emailDomainId.Value;
        personEmail.EmailUser = emailUser.Value;
        personEmail.IsPrimary = isPrimary.Value;

        await _personEmails.UpdateAsync(personEmail, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
