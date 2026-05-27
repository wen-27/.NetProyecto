using FluentValidation;

namespace Application.UseCase.PersonEmails;

public sealed class UpdatePersonEmailValidator : AbstractValidator<UpdatePersonEmail>
{
    public UpdatePersonEmailValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El identificador del correo debe ser mayor que cero.");
        RuleFor(x => x.EmailDomainId).GreaterThan(0).WithMessage("El identificador del dominio debe ser mayor que cero.");
        RuleFor(x => x.EmailUser).NotEmpty().MaximumLength(100).WithMessage("El usuario del correo es obligatorio y no puede superar 100 caracteres.");
    }
}
