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
