// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con UpdatePersonValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.Persons;

public sealed class UpdatePersonValidator : AbstractValidator<UpdatePerson>
{
    public UpdatePersonValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador de la persona debe ser mayor que cero.");

        RuleFor(x => x.FirstNames)
            .NotEmpty().WithMessage("Los nombres son obligatorios.")
            .MaximumLength(100).WithMessage("Los nombres no pueden superar 100 caracteres.");

        RuleFor(x => x.LastNames)
            .NotEmpty().WithMessage("Los apellidos son obligatorios.")
            .MaximumLength(100).WithMessage("Los apellidos no pueden superar 100 caracteres.");
    }
}
