using FluentValidation;

namespace Application.UseCase.Users;

public sealed class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.PersonId).GreaterThan(0).WithMessage("El identificador de la persona debe ser mayor que cero.");
        RuleFor(x => x.PasswordHash).NotEmpty().MaximumLength(255).WithMessage("La contraseña hasheada es obligatoria y no puede superar 255 caracteres.");
    }
}
