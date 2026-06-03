using FluentValidation;

namespace Application.UseCase.VehicleBrands;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateVehicleBrand.
public sealed class CreateVehicleBrandValidator : AbstractValidator<CreateVehicleBrand>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public CreateVehicleBrandValidator()
    {
        RuleFor(x => x.BrandName)
            .NotEmpty().WithMessage("El nombre de la marca es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre de la marca no puede superar 80 caracteres.");
    }
}
