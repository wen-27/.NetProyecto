// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con AssignUserRoleValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.UserRoles;

public sealed class AssignUserRoleValidator : AbstractValidator<AssignUserRole>
{
    public AssignUserRoleValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("El identificador del usuario debe ser mayor que cero.");
        RuleFor(x => x.RoleId).GreaterThan(0).WithMessage("El identificador del rol debe ser mayor que cero.");
    }
}
