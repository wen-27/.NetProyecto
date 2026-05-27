using FluentValidation;

namespace Application.UseCase.OrderPartDetails;

public sealed class AddPartToServiceOrderValidator : AbstractValidator<AddPartToServiceOrder>
{
    public AddPartToServiceOrderValidator()
    {
        RuleFor(x => x.ServiceOrderId).GreaterThan(0).WithMessage("El identificador de la orden debe ser mayor que cero.");
        RuleFor(x => x.PartId).GreaterThan(0).WithMessage("El identificador del repuesto debe ser mayor que cero.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("La cantidad debe ser mayor que cero.");
        RuleFor(x => x.AppliedUnitPrice).GreaterThanOrEqualTo(0).WithMessage("El precio aplicado no puede ser negativo.");
    }
}
