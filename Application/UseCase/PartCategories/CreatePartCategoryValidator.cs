using FluentValidation;

namespace Application.UseCase.PartCategories;

public sealed class CreatePartCategoryValidator : AbstractValidator<CreatePartCategory>
{
    public CreatePartCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la categoría es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre de la categoría no puede superar 80 caracteres.");
    }
}
