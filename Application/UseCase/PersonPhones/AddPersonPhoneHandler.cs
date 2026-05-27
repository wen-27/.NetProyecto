using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.PersonPhone;
using MediatR;

namespace Application.UseCase.PersonPhones;

public sealed class AddPersonPhoneHandler : IRequestHandler<AddPersonPhone, int>
{
    private readonly IPersonPhoneRepository _personPhones;
    private readonly IUnitOfWork _unitOfWork;

    public AddPersonPhoneHandler(IPersonPhoneRepository personPhones, IUnitOfWork unitOfWork)
    {
        _personPhones = personPhones;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(AddPersonPhone request, CancellationToken ct)
    {
        var personId = new PersonPhonePersonId(request.PersonId);
        var phoneCodeId = new PersonPhoneCodeId(request.PhoneCodeId);
        var phoneNumber = new PersonPhoneNumber(request.PhoneNumber);
        var isPrimary = new PersonPhoneIsPrimary(request.IsPrimary);

        if (await _personPhones.ExistsPhoneAsync(phoneCodeId, phoneNumber, ct))
        {
            throw new InvalidOperationException("Ya existe ese teléfono.");
        }

        var personPhone = new PersonPhone
        {
            PersonId = personId.Value,
            PhoneCodeId = phoneCodeId.Value,
            PhoneNumber = phoneNumber.Value,
            IsPrimary = isPrimary.Value
        };

        await _personPhones.AddAsync(personPhone, ct);
        await _unitOfWork.CommitAsync(ct);

        return personPhone.Id;
    }
}
