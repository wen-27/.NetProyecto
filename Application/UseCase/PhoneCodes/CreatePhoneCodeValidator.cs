using FluentValidation;

namespace Application.UseCase.PhoneCodes;

public sealed class CreatePhoneCodeValidator : AbstractValidator<CreatePhoneCode>
{
    public CreatePhoneCodeValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El código telefónico es obligatorio.")
            .MaximumLength(10).WithMessage("El código telefónico no puede superar 10 caracteres.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("El país es obligatorio.")
            .MaximumLength(80).WithMessage("El país no puede superar 80 caracteres.");
    }
}
