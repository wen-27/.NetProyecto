using FluentValidation;

namespace Application.UseCase.VehicleOwnerHistory;

// Caso de uso que modela una accion o consulta de negocio relacionada con RegisterVehicleOwner.
public sealed class RegisterVehicleOwnerValidator : AbstractValidator<RegisterVehicleOwner>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public RegisterVehicleOwnerValidator()
    {
        RuleFor(x => x.VehicleId).GreaterThan(0).WithMessage("El identificador del vehículo debe ser mayor que cero.");
        RuleFor(x => x.PersonId).GreaterThan(0).WithMessage("El identificador de la persona debe ser mayor que cero.");
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("La fecha de inicio es obligatoria.");
    }
}
