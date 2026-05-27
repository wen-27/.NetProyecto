using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleType;

public readonly record struct VehicleTypeName
{
    public VehicleTypeName(string value) => Value = ValueObjectValidation.Required(value, nameof(VehicleTypeName), 80);
    public string Value { get; }
}
