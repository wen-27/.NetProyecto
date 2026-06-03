// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con AddPersonEmailHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.PersonEmail;
using MediatR;

namespace Application.UseCase.PersonEmails;

public sealed class AddPersonEmailHandler : IRequestHandler<AddPersonEmail, int>
{
    private readonly IPersonEmailRepository _personEmails;
    private readonly IUnitOfWork _unitOfWork;

    public AddPersonEmailHandler(IPersonEmailRepository personEmails, IUnitOfWork unitOfWork)
    {
        _personEmails = personEmails;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(AddPersonEmail request, CancellationToken ct)
    {
        var personId = new PersonEmailPersonId(request.PersonId);
        var emailDomainId = new PersonEmailDomainId(request.EmailDomainId);
        var emailUser = new PersonEmailUser(request.EmailUser);
        var isPrimary = new PersonEmailIsPrimary(request.IsPrimary);

        if (await _personEmails.ExistsEmailAsync(emailUser, emailDomainId, ct))
        {
            throw new InvalidOperationException("Ya existe ese correo.");
        }

        var personEmail = new PersonEmail
        {
            PersonId = personId.Value,
            EmailDomainId = emailDomainId.Value,
            EmailUser = emailUser.Value,
            IsPrimary = isPrimary.Value
        };

        await _personEmails.AddAsync(personEmail, ct);
        await _unitOfWork.CommitAsync(ct);

        return personEmail.Id;
    }
}
