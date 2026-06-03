// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con UpdateServiceTypeValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.ServiceTypes;

public sealed class UpdateServiceTypeValidator : AbstractValidator<UpdateServiceType>
{
    public UpdateServiceTypeValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador del tipo de servicio debe ser mayor que cero.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del tipo de servicio es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre del tipo de servicio no puede superar 80 caracteres.");
    }
}
