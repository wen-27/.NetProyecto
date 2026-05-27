using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceType;

public readonly record struct ServiceTypeEstimatedDays
{
    public ServiceTypeEstimatedDays(int value) => Value = ValueObjectValidation.Positive(value, nameof(ServiceTypeEstimatedDays));
    public int Value { get; }
}
