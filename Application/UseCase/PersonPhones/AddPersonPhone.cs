using MediatR;

namespace Application.UseCase.PersonPhones;

public sealed record AddPersonPhone(int PersonId, int PhoneCodeId, string PhoneNumber, bool IsPrimary) : IRequest<int>;
