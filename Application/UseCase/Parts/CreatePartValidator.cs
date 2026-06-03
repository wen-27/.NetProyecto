// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreatePartValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.Parts;

public sealed class CreatePartValidator : AbstractValidator<CreatePart>
{
    public CreatePartValidator()
    {
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
