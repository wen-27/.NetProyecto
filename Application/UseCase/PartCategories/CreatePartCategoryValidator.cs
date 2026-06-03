// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreatePartCategoryValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
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
