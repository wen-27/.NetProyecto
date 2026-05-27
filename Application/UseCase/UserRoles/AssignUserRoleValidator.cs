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
