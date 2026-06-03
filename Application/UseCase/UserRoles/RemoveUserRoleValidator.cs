using FluentValidation;

namespace Application.UseCase.UserRoles;

// Caso de uso que modela una accion o consulta de negocio relacionada con RemoveUserRole.
public sealed class RemoveUserRoleValidator : AbstractValidator<RemoveUserRole>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public RemoveUserRoleValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("El identificador del usuario debe ser mayor que cero.");
        RuleFor(x => x.RoleId).GreaterThan(0).WithMessage("El identificador del rol debe ser mayor que cero.");
    }
}
