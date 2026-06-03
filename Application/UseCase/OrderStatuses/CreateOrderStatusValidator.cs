// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreateOrderStatusValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
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
