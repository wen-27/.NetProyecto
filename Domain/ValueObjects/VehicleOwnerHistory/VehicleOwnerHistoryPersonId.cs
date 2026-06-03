using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleOwnerHistory;

// Value Object que encapsula y valida un valor especifico de VehicleOwnerHistoryPersonId.
public readonly record struct VehicleOwnerHistoryPersonId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleOwnerHistoryPersonId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(VehicleOwnerHistoryPersonId));
    }

    public int Value { get; }
}
