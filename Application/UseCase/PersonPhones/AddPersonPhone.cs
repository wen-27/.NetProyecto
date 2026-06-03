using MediatR;

namespace Application.UseCase.PersonPhones;

// Caso de uso que modela una accion o consulta de negocio relacionada con AddPersonPhone.
public sealed record AddPersonPhone(int PersonId, int CountryId, string PhoneNumber, bool IsPrimary) : IRequest<int>;
