using MediatR;

namespace Application.UseCase.PersonPhones;

public sealed record UpdatePersonPhone(int Id, int CountryId, string PhoneNumber, bool IsPrimary) : IRequest;
