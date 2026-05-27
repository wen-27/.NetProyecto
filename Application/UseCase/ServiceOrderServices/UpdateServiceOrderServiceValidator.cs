using FluentValidation;

namespace Application.UseCase.ServiceOrderServices;

public sealed class UpdateServiceOrderServiceValidator : AbstractValidator<UpdateServiceOrderService>
{
    public UpdateServiceOrderServiceValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El identificador del servicio de la orden debe ser mayor que cero.");
        RuleFor(x => x.ServiceTypeId).GreaterThan(0).WithMessage("El identificador del tipo de servicio debe ser mayor que cero.");
        RuleFor(x => x.MechanicId).GreaterThan(0).WithMessage("El identificador del mecánico debe ser mayor que cero.");
        RuleFor(x => x.Description).MaximumLength(500).WithMessage("La descripción no puede superar 500 caracteres.");
        RuleFor(x => x.LaborCost).GreaterThanOrEqualTo(0).WithMessage("El costo de mano de obra no puede ser negativo.");
    }
}
