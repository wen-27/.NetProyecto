// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con RegisterAuditValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.Audits;

public sealed class RegisterAuditValidator : AbstractValidator<RegisterAudit>
{
    public RegisterAuditValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("El identificador del usuario debe ser mayor que cero.");
        RuleFor(x => x.AuditActionTypeId).GreaterThan(0).WithMessage("El identificador del tipo de acción debe ser mayor que cero.");
        RuleFor(x => x.AffectedEntity).NotEmpty().MaximumLength(100).WithMessage("La entidad afectada es obligatoria y no puede superar 100 caracteres.");
        RuleFor(x => x.AffectedRecordId).GreaterThan(0).WithMessage("El registro afectado debe ser mayor que cero.");
        RuleFor(x => x.Description).MaximumLength(5000).WithMessage("La descripción no puede superar 5000 caracteres.");
    }
}
