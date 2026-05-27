using FluentValidation;

namespace Application.UseCase.Vehicles;

public sealed class UpdateVehicleValidator : AbstractValidator<UpdateVehicle>
{
    public UpdateVehicleValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador del vehículo debe ser mayor que cero.");

        RuleFor(x => x.ModelId)
            .GreaterThan(0).WithMessage("El identificador del modelo debe ser mayor que cero.");

        RuleFor(x => x.VehicleTypeId)
            .GreaterThan(0).WithMessage("El identificador del tipo de vehículo debe ser mayor que cero.");

        RuleFor(x => x.Vin)
            .NotEmpty().WithMessage("El VIN es obligatorio.")
            .Length(17).WithMessage("El VIN debe tener exactamente 17 caracteres.");

        RuleFor(x => x.Year)
            .InclusiveBetween(1886, DateTime.UtcNow.Year + 1).WithMessage("El año del vehículo está fuera del rango válido.");

        RuleFor(x => x.Mileage)
            .GreaterThanOrEqualTo(0).WithMessage("El kilometraje no puede ser negativo.");
    }
}
