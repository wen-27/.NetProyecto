using FluentValidation;

namespace Application.UseCase.ServiceOrderServices;

public sealed class RemoveServiceFromOrderValidator : AbstractValidator<RemoveServiceFromOrder>
{
    public RemoveServiceFromOrderValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El identificador del servicio de la orden debe ser mayor que cero.");
    }
}
