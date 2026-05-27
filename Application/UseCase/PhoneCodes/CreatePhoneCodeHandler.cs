using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.PhoneCode;
using MediatR;

namespace Application.UseCase.PhoneCodes;

public sealed class CreatePhoneCodeHandler : IRequestHandler<CreatePhoneCode, int>
{
    private readonly IPhoneCodeRepository _phoneCodes;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePhoneCodeHandler(IPhoneCodeRepository phoneCodes, IUnitOfWork unitOfWork)
    {
        _phoneCodes = phoneCodes;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreatePhoneCode request, CancellationToken ct)
    {
        var code = new PhoneCodeCode(request.Code);
        var country = new PhoneCodeCountry(request.Country);

        if (await _phoneCodes.ExistsCodeAsync(code, ct))
        {
            throw new InvalidOperationException("Ya existe ese código telefónico.");
        }

        var phoneCode = new PhoneCode
        {
            Code = code.Value,
            Country = country.Value
        };

        await _phoneCodes.AddAsync(phoneCode, ct);
        await _unitOfWork.CommitAsync(ct);

        return phoneCode.Id;
    }
}
