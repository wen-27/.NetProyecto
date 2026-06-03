// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con ChangeUserStatusValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.Users;

public sealed class ChangeUserStatusValidator : AbstractValidator<ChangeUserStatus>
{
    public ChangeUserStatusValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El identificador del usuario debe ser mayor que cero.");
    }
}
