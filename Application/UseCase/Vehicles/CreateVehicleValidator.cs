// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreateVehicleValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.Vehicles;

public sealed class CreateVehicleValidator : AbstractValidator<CreateVehicle>
{
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
