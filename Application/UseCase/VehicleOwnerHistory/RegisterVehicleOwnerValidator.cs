using FluentValidation;

namespace Application.UseCase.VehicleOwnerHistory;

public sealed class RegisterVehicleOwnerValidator : AbstractValidator<RegisterVehicleOwner>
{
    public RegisterVehicleOwnerValidator()
    {
        RuleFor(x => x.VehicleId).GreaterThan(0).WithMessage("El identificador del vehículo debe ser mayor que cero.");
        RuleFor(x => x.PersonId).GreaterThan(0).WithMessage("El identificador de la persona debe ser mayor que cero.");
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("La fecha de inicio es obligatoria.");
    }
}
