using FluentValidation;

namespace Application.UseCase.EmailDomains;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateEmailDomain.
public sealed class UpdateEmailDomainValidator : AbstractValidator<UpdateEmailDomain>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public UpdateEmailDomainValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador del dominio debe ser mayor que cero.");

        RuleFor(x => x.Domain)
            .NotEmpty().WithMessage("El dominio de correo es obligatorio.")
            .MaximumLength(100).WithMessage("El dominio de correo no puede superar 100 caracteres.");
    }
}
