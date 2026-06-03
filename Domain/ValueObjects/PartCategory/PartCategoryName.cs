// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PartCategoryName, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.PartCategory;

public readonly record struct PartCategoryName
{
    public PartCategoryName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PartCategoryName), 80);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
