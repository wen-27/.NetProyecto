using FluentValidation;

namespace Application.UseCase.VehicleOwnerHistory;

// Caso de uso que modela una accion o consulta de negocio relacionada con EndVehicleOwnership.
public sealed class EndVehicleOwnershipValidator : AbstractValidator<EndVehicleOwnership>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public EndVehicleOwnershipValidator()
    {
        RuleFor(x => x.VehicleId).GreaterThan(0).WithMessage("El identificador del vehículo debe ser mayor que cero.");
        RuleFor(x => x.EndDate).NotEmpty().WithMessage("La fecha de fin es obligatoria.");
    }
}
