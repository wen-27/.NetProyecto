using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrder;

// Value Object que encapsula y valida un valor especifico de ServiceOrderWorkPerformed.
public readonly record struct ServiceOrderWorkPerformed
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public string? Value { get; }

    public ServiceOrderWorkPerformed(string? value)
    {
        Value = ValueObjectValidation.Optional(value, nameof(ServiceOrderWorkPerformed), 5000);
    }

    public bool IsEmpty => string.IsNullOrWhiteSpace(Value);

    public override string ToString() => Value ?? string.Empty;
}
