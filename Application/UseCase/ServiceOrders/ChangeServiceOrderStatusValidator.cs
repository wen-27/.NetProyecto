using FluentValidation;

namespace Application.UseCase.ServiceOrders;

// Caso de uso que modela una accion o consulta de negocio relacionada con ChangeServiceOrderStatus.
public sealed class ChangeServiceOrderStatusValidator : AbstractValidator<ChangeServiceOrderStatus>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public ChangeServiceOrderStatusValidator()
    {
        RuleFor(x => x.ServiceOrderId).GreaterThan(0).WithMessage("El identificador de la orden debe ser mayor que cero.");
        RuleFor(x => x.OrderStatusId).GreaterThan(0).WithMessage("El identificador del estado debe ser mayor que cero.");
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("El identificador del usuario debe ser mayor que cero.");
        RuleFor(x => x.Observation).MaximumLength(500).WithMessage("La observación no puede superar 500 caracteres.");
    }
}
