using FluentValidation;

namespace Application.UseCase.EmailDomains;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateEmailDomain.
public sealed class CreateEmailDomainValidator : AbstractValidator<CreateEmailDomain>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public CreateEmailDomainValidator()
    {
        RuleFor(x => x.Domain)
            .NotEmpty().WithMessage("El dominio de correo es obligatorio.")
            .MaximumLength(100).WithMessage("El dominio de correo no puede superar 100 caracteres.");
    }
}
