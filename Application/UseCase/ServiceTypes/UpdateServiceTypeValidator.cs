using FluentValidation;

namespace Application.UseCase.ServiceTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateServiceType.
public sealed class UpdateServiceTypeValidator : AbstractValidator<UpdateServiceType>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public UpdateServiceTypeValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador del tipo de servicio debe ser mayor que cero.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del tipo de servicio es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre del tipo de servicio no puede superar 80 caracteres.");
    }
}
