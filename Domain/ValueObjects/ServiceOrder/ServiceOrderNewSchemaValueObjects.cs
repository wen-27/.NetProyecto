using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrder;

// Value Object que encapsula y valida un valor especifico de ServiceOrderGeneralDescription.
public readonly record struct ServiceOrderGeneralDescription
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public ServiceOrderGeneralDescription(string? value) => Value = ValueObjectValidation.Optional(value, nameof(ServiceOrderGeneralDescription), 2000);
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de ServiceOrderCancellationReason.
public readonly record struct ServiceOrderCancellationReason
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public ServiceOrderCancellationReason(string? value) => Value = ValueObjectValidation.Optional(value, nameof(ServiceOrderCancellationReason), 1000);
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de ServiceOrderCancellationDate.
public readonly record struct ServiceOrderCancellationDate
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public ServiceOrderCancellationDate(DateTime? value) => Value = value;
    public DateTime? Value { get; }
}
