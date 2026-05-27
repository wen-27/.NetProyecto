using FluentValidation;

namespace Application.UseCase.Users;

public sealed class ChangeUserStatusValidator : AbstractValidator<ChangeUserStatus>
{
    public ChangeUserStatusValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El identificador del usuario debe ser mayor que cero.");
    }
}
