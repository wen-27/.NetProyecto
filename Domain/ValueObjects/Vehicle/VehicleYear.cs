// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de VehicleYear, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
namespace Domain.ValueObjects.Vehicle;

public readonly record struct VehicleYear
{
    public VehicleYear(int value)
    {
        if (value < 1886 || value > DateTime.UtcNow.Year + 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "El año del vehículo está fuera del rango válido.");
        }

        Value = value;
    }

    public int Value { get; }
}
