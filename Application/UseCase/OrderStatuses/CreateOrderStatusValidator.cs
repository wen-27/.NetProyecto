using FluentValidation;

namespace Application.UseCase.OrderStatuses;

public sealed class CreateOrderStatusValidator : AbstractValidator<CreateOrderStatus>
{
    public CreateOrderStatusValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del estado de orden es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre del estado de orden no puede superar 50 caracteres.");
    }
}
