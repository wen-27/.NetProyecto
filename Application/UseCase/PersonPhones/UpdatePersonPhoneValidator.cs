using FluentValidation;

namespace Application.UseCase.PersonPhones;

public sealed class UpdatePersonPhoneValidator : AbstractValidator<UpdatePersonPhone>
{
    public UpdatePersonPhoneValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El identificador del teléfono debe ser mayor que cero.");
        RuleFor(x => x.CountryId).GreaterThan(0).WithMessage("El identificador del país debe ser mayor que cero.");
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(30).WithMessage("El teléfono es obligatorio y no puede superar 30 caracteres.");
    }
}
