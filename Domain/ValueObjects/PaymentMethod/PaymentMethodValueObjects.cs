using Domain.ValueObjects;

namespace Domain.ValueObjects.PaymentMethod;

// Value Object que encapsula y valida un valor especifico de PaymentMethodName.
public readonly record struct PaymentMethodName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PaymentMethodName(string value) => Value = ValueObjectValidation.Required(value, nameof(PaymentMethodName), 50);
    public string Value { get; }
}
