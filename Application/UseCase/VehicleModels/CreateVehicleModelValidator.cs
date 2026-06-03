// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreateVehicleModelValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.VehicleModels;

public sealed class CreateVehicleModelValidator : AbstractValidator<CreateVehicleModel>
{
    public CreateVehicleModelValidator()
    {
        RuleFor(x => x.BrandId)
            .GreaterThan(0).WithMessage("El identificador de la marca debe ser mayor que cero.");

        RuleFor(x => x.ModelName)
            .NotEmpty().WithMessage("El nombre del modelo es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre del modelo no puede superar 80 caracteres.");
    }
}
