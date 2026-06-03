using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleModel;

// Value Object que encapsula y valida un valor especifico de VehicleModelName.
public readonly record struct VehicleModelName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleModelName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(VehicleModelName), 80);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
