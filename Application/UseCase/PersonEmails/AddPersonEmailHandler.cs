using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.PersonEmail;
using MediatR;

namespace Application.UseCase.PersonEmails;

// Caso de uso que modela una accion o consulta de negocio relacionada con AddPersonEmail.
public sealed class AddPersonEmailHandler : IRequestHandler<AddPersonEmail, int>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
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
