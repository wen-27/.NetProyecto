using FluentValidation;

namespace Application.UseCase.VehicleModels;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateVehicleModel.
public sealed class CreateVehicleModelValidator : AbstractValidator<CreateVehicleModel>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public CreateVehicleModelValidator()
    {
        RuleFor(x => x.BrandId)
            .GreaterThan(0).WithMessage("El identificador de la marca debe ser mayor que cero.");

        RuleFor(x => x.ModelName)
            .NotEmpty().WithMessage("El nombre del modelo es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre del modelo no puede superar 80 caracteres.");
    }
}
