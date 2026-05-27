using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleModel;

public readonly record struct VehicleModelName
{
    public VehicleModelName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(VehicleModelName), 80);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
