using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleOwnerHistory;

// Value Object que encapsula y valida un valor especifico de VehicleOwnerHistoryVehicleId.
public readonly record struct VehicleOwnerHistoryVehicleId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleOwnerHistoryVehicleId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(VehicleOwnerHistoryVehicleId));
    }
    public int Value { get; }
}
