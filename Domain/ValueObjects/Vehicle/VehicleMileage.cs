// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de VehicleMileage, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Vehicle;

public readonly record struct VehicleMileage
{
    public VehicleMileage(int value)
    {
        Value = ValueObjectValidation.NonNegative(value, nameof(VehicleMileage));
    }
    public int Value { get; }
}
