using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleBrand;

public readonly record struct VehicleBrandName
{
    public VehicleBrandName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(VehicleBrandName), 80);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
