using Domain.ValueObjects;

namespace Domain.ValueObjects.Vehicle;

// Value Object que encapsula y valida un valor especifico de VehicleColor.
public readonly record struct VehicleColor
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleColor(string? value)
    {
        Value = ValueObjectValidation.Optional(value, nameof(VehicleColor), 30);
    }

    public string? Value { get; }
}
