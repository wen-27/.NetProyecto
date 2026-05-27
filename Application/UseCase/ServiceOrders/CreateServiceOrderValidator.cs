using FluentValidation;
using Domain.Enums.OrderStatus;

namespace Application.UseCase.ServiceOrders;

public sealed class CreateServiceOrderValidator : AbstractValidator<CreateServiceOrder>
{
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
