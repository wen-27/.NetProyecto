using FluentValidation;

namespace Application.UseCase.OrderStatuses;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateOrderStatus.
public sealed class CreateOrderStatusValidator : AbstractValidator<CreateOrderStatus>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public CreateOrderStatusValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del estado de orden es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre del estado de orden no puede superar 50 caracteres.");
    }
}
