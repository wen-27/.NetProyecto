using FluentValidation;

namespace Application.UseCase.OrderPartDetails;

public sealed class UpdateServiceOrderPartValidator : AbstractValidator<UpdateServiceOrderPart>
{
    public UpdateServiceOrderPartValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El identificador del detalle debe ser mayor que cero.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("La cantidad debe ser mayor que cero.");
        RuleFor(x => x.AppliedUnitPrice).GreaterThanOrEqualTo(0).WithMessage("El precio aplicado no puede ser negativo.");
    }
}
