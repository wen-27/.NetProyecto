using FluentValidation;

namespace Application.UseCase.PersonPhones;

public sealed class AddPersonPhoneValidator : AbstractValidator<AddPersonPhone>
{
    public AddPersonPhoneValidator()
    {
        RuleFor(x => x.PersonId).GreaterThan(0).WithMessage("El identificador de la persona debe ser mayor que cero.");
        RuleFor(x => x.PhoneCodeId).GreaterThan(0).WithMessage("El identificador del código telefónico debe ser mayor que cero.");
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(30).WithMessage("El teléfono es obligatorio y no puede superar 30 caracteres.");
    }
}
