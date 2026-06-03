// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con UpdateDocumentTypeValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.DocumentTypes;

public sealed class UpdateDocumentTypeValidator : AbstractValidator<UpdateDocumentType>
{
    public UpdateDocumentTypeValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador del tipo de documento debe ser mayor que cero.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El código del tipo de documento es obligatorio.")
            .MaximumLength(10).WithMessage("El código del tipo de documento no puede superar 10 caracteres.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del tipo de documento es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre del tipo de documento no puede superar 80 caracteres.");
    }
}
