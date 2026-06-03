using Domain.ValueObjects;

namespace Domain.ValueObjects.PaymentStatus;

// Value Object que encapsula y valida un valor especifico de PaymentStatusName.
public readonly record struct PaymentStatusName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PaymentStatusName(string value) => Value = ValueObjectValidation.Required(value, nameof(PaymentStatusName), 50);
    public string Value { get; }
}
