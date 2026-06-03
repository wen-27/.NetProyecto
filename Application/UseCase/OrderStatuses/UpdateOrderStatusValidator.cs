using FluentValidation;

namespace Application.UseCase.OrderStatuses;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateOrderStatus.
public sealed class UpdateOrderStatusValidator : AbstractValidator<UpdateOrderStatus>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public UpdateOrderStatusValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador del estado de orden debe ser mayor que cero.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del estado de orden es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre del estado de orden no puede superar 50 caracteres.");
    }
}
