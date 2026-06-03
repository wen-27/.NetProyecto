using FluentValidation;

namespace Application.UseCase.AuditActionTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateAuditActionType.
public sealed class UpdateAuditActionTypeValidator : AbstractValidator<UpdateAuditActionType>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public UpdateAuditActionTypeValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador del tipo de acción debe ser mayor que cero.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del tipo de acción es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre del tipo de acción no puede superar 50 caracteres.");
    }
}
