using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleType;

// Value Object que encapsula y valida un valor especifico de VehicleTypeName.
public readonly record struct VehicleTypeName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleTypeName(string value) => Value = ValueObjectValidation.Required(value, nameof(VehicleTypeName), 80);
    public string Value { get; }
}
