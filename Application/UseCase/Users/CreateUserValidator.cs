using FluentValidation;

namespace Application.UseCase.Users;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateUser.
public sealed class CreateUserValidator : AbstractValidator<CreateUser>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public CreateUserValidator()
    {
        RuleFor(x => x.PersonId).GreaterThan(0).WithMessage("El identificador de la persona debe ser mayor que cero.");
        RuleFor(x => x.PasswordHash).NotEmpty().MaximumLength(255).WithMessage("La contraseña hasheada es obligatoria y no puede superar 255 caracteres.");
    }
}
