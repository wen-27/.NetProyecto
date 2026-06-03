using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrder;

// Value Object que encapsula y valida un valor especifico de ServiceOrderVehicleId.
public readonly record struct ServiceOrderVehicleId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public ServiceOrderVehicleId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(ServiceOrderVehicleId));
    }
    public int Value { get; }
}
