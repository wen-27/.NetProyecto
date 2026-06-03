using FluentValidation;

namespace Application.UseCase.Users;

// Caso de uso que modela una accion o consulta de negocio relacionada con ChangeUserStatus.
public sealed class ChangeUserStatusValidator : AbstractValidator<ChangeUserStatus>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public ChangeUserStatusValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El identificador del usuario debe ser mayor que cero.");
    }
}
