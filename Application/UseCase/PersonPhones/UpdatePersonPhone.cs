using MediatR;

namespace Application.UseCase.PersonPhones;

public sealed record UpdatePersonPhone(int Id, int PhoneCodeId, string PhoneNumber, bool IsPrimary) : IRequest;
