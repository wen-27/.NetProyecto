using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrder;

public readonly record struct ServiceOrderWorkPerformed
{
    public string? Value { get; }

    public ServiceOrderWorkPerformed(string? value)
    {
        Value = ValueObjectValidation.Optional(value, nameof(ServiceOrderWorkPerformed), 5000);
    }

    public bool IsEmpty => string.IsNullOrWhiteSpace(Value);

    public override string ToString() => Value ?? string.Empty;
}
