using FluentValidation;

namespace Application.UseCase.PartCategories;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreatePartCategory.
public sealed class CreatePartCategoryValidator : AbstractValidator<CreatePartCategory>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public CreatePartCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la categoría es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre de la categoría no puede superar 80 caracteres.");
    }
}
