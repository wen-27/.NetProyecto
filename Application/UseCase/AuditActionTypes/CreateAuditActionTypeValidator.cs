// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreateAuditActionTypeValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.AuditActionTypes;

public sealed class CreateAuditActionTypeValidator : AbstractValidator<CreateAuditActionType>
{
    public CreateAuditActionTypeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del tipo de acción es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre del tipo de acción no puede superar 50 caracteres.");
    }
}
