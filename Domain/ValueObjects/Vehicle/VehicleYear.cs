namespace Domain.ValueObjects.Vehicle;

// Value Object que encapsula y valida un valor especifico de VehicleYear.
public readonly record struct VehicleYear
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
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
