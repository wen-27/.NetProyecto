using FluentValidation;

namespace Application.UseCase.AuditActionTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateAuditActionType.
public sealed class CreateAuditActionTypeValidator : AbstractValidator<CreateAuditActionType>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public CreateAuditActionTypeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del tipo de acción es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre del tipo de acción no puede superar 50 caracteres.");
    }
}
