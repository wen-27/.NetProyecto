// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PartBrandId, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

public readonly record struct PartBrandId
{
    public PartBrandId(int? value)
    {
        if (value is not null && value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "El identificador de la marca del repuesto debe ser mayor que cero.");
        }

        Value = value;
    }

    public int? Value { get; }
}
