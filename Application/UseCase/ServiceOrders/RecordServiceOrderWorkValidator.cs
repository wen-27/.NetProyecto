// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con RecordServiceOrderWorkValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.ServiceOrders;

public sealed class RecordServiceOrderWorkValidator : AbstractValidator<RecordServiceOrderWork>
{
    public RecordServiceOrderWorkValidator()
    {
        RuleFor(x => x.ServiceOrderId).GreaterThan(0).WithMessage("El identificador de la orden debe ser mayor que cero.");
        RuleFor(x => x.WorkPerformed).NotEmpty().MaximumLength(5000).WithMessage("El trabajo realizado es obligatorio y no puede superar 5000 caracteres.");
    }
}
