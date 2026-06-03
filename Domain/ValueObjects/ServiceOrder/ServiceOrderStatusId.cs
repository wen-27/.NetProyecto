using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrder;

// Value Object que encapsula y valida un valor especifico de ServiceOrderStatusId.
public readonly record struct ServiceOrderStatusId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public ServiceOrderStatusId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(ServiceOrderStatusId));
    }
    public int Value { get; }
}
