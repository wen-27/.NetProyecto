using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleBrand;

// Value Object que encapsula y valida un valor especifico de VehicleBrandName.
public readonly record struct VehicleBrandName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleBrandName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(VehicleBrandName), 80);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
