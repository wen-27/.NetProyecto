using Domain.ValueObjects;

namespace Domain.ValueObjects.Vehicle;

// Value Object que encapsula y valida un valor especifico de VehicleVin.
public readonly record struct VehicleVin
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
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
