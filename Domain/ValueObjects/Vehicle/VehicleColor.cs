// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de VehicleColor, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Vehicle;

public readonly record struct VehicleColor
{
    public VehicleColor(string? value)
    {
        Value = ValueObjectValidation.Optional(value, nameof(VehicleColor), 30);
    }

    public string? Value { get; }
}
