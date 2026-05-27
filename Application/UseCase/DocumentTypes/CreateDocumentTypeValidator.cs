using FluentValidation;

namespace Application.UseCase.DocumentTypes;

public sealed class CreateDocumentTypeValidator : AbstractValidator<CreateDocumentType>
{
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
