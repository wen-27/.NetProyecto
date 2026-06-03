using MediatR;

namespace Application.UseCase.PersonPhones;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdatePersonPhone.
public sealed record UpdatePersonPhone(int Id, int CountryId, string PhoneNumber, bool IsPrimary) : IRequest;
