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
