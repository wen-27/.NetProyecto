using FluentValidation;

namespace Application.UseCase.EmailDomains;

public sealed class UpdateEmailDomainValidator : AbstractValidator<UpdateEmailDomain>
{
    public UpdateEmailDomainValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador del dominio debe ser mayor que cero.");

        RuleFor(x => x.Domain)
            .NotEmpty().WithMessage("El dominio de correo es obligatorio.")
            .MaximumLength(100).WithMessage("El dominio de correo no puede superar 100 caracteres.");
    }
}
