// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con EndVehicleOwnershipValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
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
