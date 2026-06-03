using Domain.ValueObjects;

namespace Domain.ValueObjects.Vehicle;

// Value Object que encapsula y valida un valor especifico de VehiclePlate.
public readonly record struct VehiclePlate
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehiclePlate(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(VehiclePlate), 10).ToUpperInvariant();
    }

    public string Value { get; }
    public override string ToString() => Value;
}
