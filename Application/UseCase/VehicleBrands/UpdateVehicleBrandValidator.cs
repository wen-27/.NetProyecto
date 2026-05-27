using FluentValidation;

namespace Application.UseCase.VehicleBrands;

public sealed class UpdateVehicleBrandValidator : AbstractValidator<UpdateVehicleBrand>
{
    public UpdateVehicleBrandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador de la marca debe ser mayor que cero.");

        RuleFor(x => x.BrandName)
            .NotEmpty().WithMessage("El nombre de la marca es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre de la marca no puede superar 80 caracteres.");
    }
}
