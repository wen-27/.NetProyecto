// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de VehiclePlate, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Vehicle;

public readonly record struct VehiclePlate
{
    public VehiclePlate(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(VehiclePlate), 10).ToUpperInvariant();
    }

    public string Value { get; }
    public override string ToString() => Value;
}
