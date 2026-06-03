using FluentValidation;

namespace Application.UseCase.Persons;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreatePerson.
public sealed class CreatePersonValidator : AbstractValidator<CreatePerson>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public CreatePersonValidator()
    {
        RuleFor(x => x.FirstNames)
            .NotEmpty().WithMessage("Los nombres son obligatorios.")
            .MaximumLength(100).WithMessage("Los nombres no pueden superar 100 caracteres.");

        RuleFor(x => x.LastNames)
            .NotEmpty().WithMessage("Los apellidos son obligatorios.")
            .MaximumLength(100).WithMessage("Los apellidos no pueden superar 100 caracteres.");
    }
}
