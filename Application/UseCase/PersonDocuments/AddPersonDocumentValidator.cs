using FluentValidation;

namespace Application.UseCase.PersonDocuments;

public sealed class AddPersonDocumentValidator : AbstractValidator<AddPersonDocument>
{
    public AddPersonDocumentValidator()
    {
        RuleFor(x => x.PersonId).GreaterThan(0).WithMessage("El identificador de la persona debe ser mayor que cero.");
        RuleFor(x => x.DocumentTypeId).GreaterThan(0).WithMessage("El identificador del tipo de documento debe ser mayor que cero.");
        RuleFor(x => x.DocumentNumber).NotEmpty().MaximumLength(50).WithMessage("El número de documento es obligatorio y no puede superar 50 caracteres.");
    }
}
