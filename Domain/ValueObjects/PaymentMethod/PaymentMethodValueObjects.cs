using Domain.ValueObjects;

namespace Domain.ValueObjects.PaymentMethod;

public readonly record struct PaymentMethodName
{
    public PaymentMethodName(string value) => Value = ValueObjectValidation.Required(value, nameof(PaymentMethodName), 50);
    public string Value { get; }
}
