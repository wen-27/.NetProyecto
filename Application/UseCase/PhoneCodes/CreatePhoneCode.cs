using MediatR;

namespace Application.UseCase.PhoneCodes;

public sealed record CreatePhoneCode(string Code, string Country) : IRequest<int>;
