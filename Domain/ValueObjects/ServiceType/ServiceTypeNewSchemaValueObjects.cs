using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceType;

// Value Object que encapsula y valida un valor especifico de ServiceTypeEstimatedDays.
public readonly record struct ServiceTypeEstimatedDays
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public ServiceTypeEstimatedDays(int value) => Value = ValueObjectValidation.Positive(value, nameof(ServiceTypeEstimatedDays));
    public int Value { get; }
}
