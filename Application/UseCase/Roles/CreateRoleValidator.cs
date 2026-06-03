using FluentValidation;

namespace Application.UseCase.Roles;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateRole.
public sealed class CreateRoleValidator : AbstractValidator<CreateRole>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public CreateRoleValidator()
    {
        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("El nombre del rol es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre del rol no puede superar 50 caracteres.");
    }
}
