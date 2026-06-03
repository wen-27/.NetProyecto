// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con UpdateAuditActionTypeValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.AuditActionTypes;

public sealed class UpdateAuditActionTypeValidator : AbstractValidator<UpdateAuditActionType>
{
    public UpdateAuditActionTypeValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador del tipo de acción debe ser mayor que cero.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del tipo de acción es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre del tipo de acción no puede superar 50 caracteres.");
    }
}
