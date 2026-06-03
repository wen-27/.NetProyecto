// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de VehicleVin, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Vehicle;

public readonly record struct VehicleVin
{
    public VehicleVin(string value)
    {
        value = ValueObjectValidation.Required(value, nameof(VehicleVin), 17).ToUpperInvariant();
        if (value.Length != 17)
        {
            throw new ArgumentException("El VIN del vehículo debe tener exactamente 17 caracteres.", nameof(value));
        }

        Value = value;
    }

    public string Value { get; }
    public override string ToString() => Value;
}
