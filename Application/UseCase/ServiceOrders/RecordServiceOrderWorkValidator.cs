using FluentValidation;

namespace Application.UseCase.ServiceOrders;

// Caso de uso que modela una accion o consulta de negocio relacionada con RecordServiceOrderWork.
public sealed class RecordServiceOrderWorkValidator : AbstractValidator<RecordServiceOrderWork>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public RecordServiceOrderWorkValidator()
    {
        RuleFor(x => x.ServiceOrderId).GreaterThan(0).WithMessage("El identificador de la orden debe ser mayor que cero.");
        RuleFor(x => x.WorkPerformed).NotEmpty().MaximumLength(5000).WithMessage("El trabajo realizado es obligatorio y no puede superar 5000 caracteres.");
    }
}
