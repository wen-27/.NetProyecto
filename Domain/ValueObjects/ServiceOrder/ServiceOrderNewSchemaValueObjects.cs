using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrder;

public readonly record struct ServiceOrderGeneralDescription
{
    public ServiceOrderGeneralDescription(string? value) => Value = ValueObjectValidation.Optional(value, nameof(ServiceOrderGeneralDescription), 2000);
    public string? Value { get; }
}

public readonly record struct ServiceOrderCancellationReason
{
    public ServiceOrderCancellationReason(string? value) => Value = ValueObjectValidation.Optional(value, nameof(ServiceOrderCancellationReason), 1000);
    public string? Value { get; }
}

public readonly record struct ServiceOrderCancellationDate
{
    public ServiceOrderCancellationDate(DateTime? value) => Value = value;
    public DateTime? Value { get; }
}
