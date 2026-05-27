using Application.Abstractions;
using Domain.ValueObjects.PhoneCode;
using MediatR;

namespace Application.UseCase.PhoneCodes;

public sealed class UpdatePhoneCodeHandler : IRequestHandler<UpdatePhoneCode>
{
    private readonly IPhoneCodeRepository _phoneCodes;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePhoneCodeHandler(IPhoneCodeRepository phoneCodes, IUnitOfWork unitOfWork)
    {
        _phoneCodes = phoneCodes;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdatePhoneCode request, CancellationToken ct)
    {
        var phoneCode = await _phoneCodes.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el código telefónico.");

        var code = new PhoneCodeCode(request.Code);
        var country = new PhoneCodeCountry(request.Country);

        phoneCode.Code = code.Value;
        phoneCode.Country = country.Value;

        await _phoneCodes.UpdateAsync(phoneCode, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
