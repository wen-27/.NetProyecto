using FluentValidation;

namespace Application.UseCase.VehicleModels;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateVehicleModel.
public sealed class UpdateVehicleModelValidator : AbstractValidator<UpdateVehicleModel>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public UpdateVehicleModelValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador del modelo debe ser mayor que cero.");

        RuleFor(x => x.BrandId)
            .GreaterThan(0).WithMessage("El identificador de la marca debe ser mayor que cero.");

        RuleFor(x => x.ModelName)
            .NotEmpty().WithMessage("El nombre del modelo es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre del modelo no puede superar 80 caracteres.");
    }
}
