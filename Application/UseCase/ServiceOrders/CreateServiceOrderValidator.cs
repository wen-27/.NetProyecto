using FluentValidation;
using Domain.Enums.OrderStatus;

namespace Application.UseCase.ServiceOrders;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateServiceOrder.
public sealed class CreateServiceOrderValidator : AbstractValidator<CreateServiceOrder>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public CreateServiceOrderValidator()
    {
        RuleFor(x => x.VehicleId).GreaterThan(0).WithMessage("El identificador del vehículo debe ser mayor que cero.");
        RuleFor(x => x.OrderStatusId).GreaterThan(0).WithMessage("El identificador del estado debe ser mayor que cero.");

        RuleFor(x => x.OrderStatusId)
            .Equal((int)ServiceOrderStatus.Pending)
            .WithMessage("Una orden nueva debe iniciar en estado pendiente.");

        RuleFor(x => x.EstimatedDeliveryDate)
            .Must(date => !date.HasValue || date.Value > DateTime.UtcNow)
            .WithMessage("La fecha estimada de entrega debe estar en el futuro.");
    }
}
