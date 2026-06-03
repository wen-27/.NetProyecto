using Domain.ValueObjects;

namespace Domain.ValueObjects.PaymentCard;

// Value Object que encapsula y valida un valor especifico de PaymentCardPaymentId.
public readonly record struct PaymentCardPaymentId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PaymentCardPaymentId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PaymentCardPaymentId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PaymentCardCardTypeId.
public readonly record struct PaymentCardCardTypeId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PaymentCardCardTypeId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PaymentCardCardTypeId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PaymentCardLastFourDigits.
public readonly record struct PaymentCardLastFourDigits
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PaymentCardLastFourDigits(string value)
    {
        value = ValueObjectValidation.Required(value, nameof(PaymentCardLastFourDigits), 4);
        if (value.Length != 4 || !value.All(char.IsDigit))
        {
            throw new ArgumentException("Los ultimos cuatro digitos de la tarjeta deben tener exactamente 4 numeros.", nameof(value));
        }

        Value = value;
    }

    public string Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PaymentCardCardHolder.
public readonly record struct PaymentCardCardHolder
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PaymentCardCardHolder(string value) => Value = ValueObjectValidation.Required(value, nameof(PaymentCardCardHolder), 100);
    public string Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PaymentCardAuthorizationCode.
public readonly record struct PaymentCardAuthorizationCode
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PaymentCardAuthorizationCode(string? value) => Value = ValueObjectValidation.Optional(value, nameof(PaymentCardAuthorizationCode), 100);
    public string? Value { get; }
}
