using FluentValidation;

namespace Application.UseCase.ServiceTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateServiceType.
public sealed class CreateServiceTypeValidator : AbstractValidator<CreateServiceType>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public CreateServiceTypeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del tipo de servicio es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre del tipo de servicio no puede superar 80 caracteres.");
    }
}
