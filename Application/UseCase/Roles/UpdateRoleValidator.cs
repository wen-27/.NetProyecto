// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con UpdateRoleValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.Roles;

public sealed class UpdateRoleValidator : AbstractValidator<UpdateRole>
{
    public UpdateRoleValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador del rol debe ser mayor que cero.");

        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("El nombre del rol es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre del rol no puede superar 50 caracteres.");
    }
}
