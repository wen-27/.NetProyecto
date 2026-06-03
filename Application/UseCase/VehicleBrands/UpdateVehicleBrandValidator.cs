using FluentValidation;

namespace Application.UseCase.VehicleBrands;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateVehicleBrand.
public sealed class UpdateVehicleBrandValidator : AbstractValidator<UpdateVehicleBrand>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public UpdateVehicleBrandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador de la marca debe ser mayor que cero.");

        RuleFor(x => x.BrandName)
            .NotEmpty().WithMessage("El nombre de la marca es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre de la marca no puede superar 80 caracteres.");
    }
}
