// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PartCode, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

public readonly record struct PartCode
{
    public PartCode(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PartCode), 50);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
