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
