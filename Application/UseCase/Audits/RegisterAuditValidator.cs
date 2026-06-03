using FluentValidation;

namespace Application.UseCase.Audits;

// Caso de uso que modela una accion o consulta de negocio relacionada con RegisterAudit.
public sealed class RegisterAuditValidator : AbstractValidator<RegisterAudit>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public RegisterAuditValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("El identificador del usuario debe ser mayor que cero.");
        RuleFor(x => x.AuditActionTypeId).GreaterThan(0).WithMessage("El identificador del tipo de acción debe ser mayor que cero.");
        RuleFor(x => x.AffectedEntity).NotEmpty().MaximumLength(100).WithMessage("La entidad afectada es obligatoria y no puede superar 100 caracteres.");
        RuleFor(x => x.AffectedRecordId).GreaterThan(0).WithMessage("El registro afectado debe ser mayor que cero.");
        RuleFor(x => x.Description).MaximumLength(5000).WithMessage("La descripción no puede superar 5000 caracteres.");
    }
}
