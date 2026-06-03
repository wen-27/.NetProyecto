// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreateVehicleBrandValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.VehicleBrands;

public sealed class CreateVehicleBrandValidator : AbstractValidator<CreateVehicleBrand>
{
    public CreateVehicleBrandValidator()
    {
        RuleFor(x => x.BrandName)
            .NotEmpty().WithMessage("El nombre de la marca es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre de la marca no puede superar 80 caracteres.");
    }
}
