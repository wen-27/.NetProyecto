using FluentValidation;

namespace Application.UseCase.Vehicles;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateVehicle.
public sealed class CreateVehicleValidator : AbstractValidator<CreateVehicle>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public CreateVehicleValidator()
    {
        RuleFor(x => x.ModelId)
            .GreaterThan(0).WithMessage("El identificador del modelo debe ser mayor que cero.");

        RuleFor(x => x.VehicleTypeId)
            .GreaterThan(0).WithMessage("El identificador del tipo de vehículo debe ser mayor que cero.");

        RuleFor(x => x.Vin)
            .NotEmpty().WithMessage("El VIN es obligatorio.")
            .Length(17).WithMessage("El VIN debe tener exactamente 17 caracteres.");

        RuleFor(x => x.Year)
            .InclusiveBetween(1886, DateTime.UtcNow.Year + 1).WithMessage("El año del vehículo está fuera del rango válido.");

        RuleFor(x => x.Color)
            .MaximumLength(30).WithMessage("El color no puede superar 30 caracteres.");

        RuleFor(x => x.Mileage)
            .GreaterThanOrEqualTo(0).WithMessage("El kilometraje no puede ser negativo.");
    }
}
