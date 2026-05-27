using Domain.ValueObjects;

namespace Domain.ValueObjects.PaymentStatus;

public readonly record struct PaymentStatusName
{
    public PaymentStatusName(string value) => Value = ValueObjectValidation.Required(value, nameof(PaymentStatusName), 50);
    public string Value { get; }
}
