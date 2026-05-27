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
