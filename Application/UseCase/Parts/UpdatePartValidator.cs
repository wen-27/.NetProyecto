using FluentValidation;

namespace Application.UseCase.Parts;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdatePart.
public sealed class UpdatePartValidator : AbstractValidator<UpdatePart>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public UpdatePartValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador del repuesto debe ser mayor que cero.");

        RuleFor(x => x.PartCategoryId)
            .GreaterThan(0).WithMessage("El identificador de la categoría debe ser mayor que cero.");

        RuleFor(x => x.PartBrandId)
            .GreaterThan(0).When(x => x.PartBrandId.HasValue)
            .WithMessage("El identificador de la marca del repuesto debe ser mayor que cero.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El código del repuesto es obligatorio.")
            .MaximumLength(50).WithMessage("El código del repuesto no puede superar 50 caracteres.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción del repuesto es obligatoria.")
            .MaximumLength(255).WithMessage("La descripción del repuesto no puede superar 255 caracteres.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo.");

        RuleFor(x => x.MinimumStock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock mínimo no puede ser negativo.");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(0).WithMessage("El precio unitario no puede ser negativo.");
    }
}
