using FluentValidation;

namespace Application.UseCase.PersonDocuments;

public sealed class UpdatePersonDocumentValidator : AbstractValidator<UpdatePersonDocument>
{
    public UpdatePersonDocumentValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El identificador del documento debe ser mayor que cero.");
        RuleFor(x => x.DocumentTypeId).GreaterThan(0).WithMessage("El identificador del tipo de documento debe ser mayor que cero.");
        RuleFor(x => x.DocumentNumber).NotEmpty().MaximumLength(50).WithMessage("El número de documento es obligatorio y no puede superar 50 caracteres.");
    }
}
