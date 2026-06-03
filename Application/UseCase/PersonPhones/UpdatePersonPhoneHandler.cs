using Application.Abstractions;
using Domain.ValueObjects.PersonPhone;
using MediatR;

namespace Application.UseCase.PersonPhones;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdatePersonPhone.
public sealed class UpdatePersonPhoneHandler : IRequestHandler<UpdatePersonPhone>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPersonPhoneRepository _personPhones;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePersonPhoneHandler(IPersonPhoneRepository personPhones, IUnitOfWork unitOfWork)
    {
        _personPhones = personPhones;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdatePersonPhone request, CancellationToken ct)
    {
        var personPhone = await _personPhones.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el teléfono de la persona.");

        var countryId = new PersonPhoneCountryId(request.CountryId);
        var phoneNumber = new PersonPhoneNumber(request.PhoneNumber);
        var isPrimary = new PersonPhoneIsPrimary(request.IsPrimary);

        personPhone.CountryId = countryId.Value;
        personPhone.PhoneNumber = phoneNumber.Value;
        personPhone.IsPrimary = isPrimary.Value;

        await _personPhones.UpdateAsync(personPhone, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
