using FluentValidation;

namespace Application.UseCase.Roles;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateRole.
public sealed class UpdateRoleValidator : AbstractValidator<UpdateRole>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public UpdateRoleValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador del rol debe ser mayor que cero.");

        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("El nombre del rol es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre del rol no puede superar 50 caracteres.");
    }
}
