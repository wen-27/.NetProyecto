using FluentValidation;

namespace Application.UseCase.VehicleOwnerHistory;

public sealed class EndVehicleOwnershipValidator : AbstractValidator<EndVehicleOwnership>
{
    public EndVehicleOwnershipValidator()
    {
        RuleFor(x => x.VehicleId).GreaterThan(0).WithMessage("El identificador del vehículo debe ser mayor que cero.");
        RuleFor(x => x.EndDate).NotEmpty().WithMessage("La fecha de fin es obligatoria.");
    }
}
