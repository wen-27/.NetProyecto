using FluentValidation;

namespace Application.UseCase.DocumentTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateDocumentType.
public sealed class CreateDocumentTypeValidator : AbstractValidator<CreateDocumentType>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public CreateDocumentTypeValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El código del tipo de documento es obligatorio.")
            .MaximumLength(10).WithMessage("El código del tipo de documento no puede superar 10 caracteres.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del tipo de documento es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre del tipo de documento no puede superar 80 caracteres.");
    }
}
