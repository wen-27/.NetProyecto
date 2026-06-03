using FluentValidation;

namespace Application.UseCase.PartCategories;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdatePartCategory.
public sealed class UpdatePartCategoryValidator : AbstractValidator<UpdatePartCategory>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public UpdatePartCategoryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador de la categoría debe ser mayor que cero.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la categoría es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre de la categoría no puede superar 80 caracteres.");
    }
}
