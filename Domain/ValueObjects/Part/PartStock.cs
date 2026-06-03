// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PartStock, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

public readonly record struct PartStock
{
    public PartStock(int value)
    {
        Value = ValueObjectValidation.NonNegative(value, nameof(PartStock));
    }
    public int Value { get; }
}
