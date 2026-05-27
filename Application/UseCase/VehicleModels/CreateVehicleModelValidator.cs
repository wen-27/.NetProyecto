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
