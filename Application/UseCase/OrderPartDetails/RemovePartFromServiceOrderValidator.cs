using FluentValidation;

namespace Application.UseCase.OrderPartDetails;

public sealed class RemovePartFromServiceOrderValidator : AbstractValidator<RemovePartFromServiceOrder>
{
    public RemovePartFromServiceOrderValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El identificador del detalle debe ser mayor que cero.");
    }
}
