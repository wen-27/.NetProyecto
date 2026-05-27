using FluentValidation;

namespace Application.UseCase.EmailDomains;

public sealed class CreateEmailDomainValidator : AbstractValidator<CreateEmailDomain>
{
    public CreateEmailDomainValidator()
    {
        RuleFor(x => x.Domain)
            .NotEmpty().WithMessage("El dominio de correo es obligatorio.")
            .MaximumLength(100).WithMessage("El dominio de correo no puede superar 100 caracteres.");
    }
}
