// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PaymentCardValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.PaymentCard;

public readonly record struct PaymentCardPaymentId
{
    public PaymentCardPaymentId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PaymentCardPaymentId));
    public int Value { get; }
}

public readonly record struct PaymentCardCardTypeId
{
    public PaymentCardCardTypeId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PaymentCardCardTypeId));
    public int Value { get; }
}

public readonly record struct PaymentCardLastFourDigits
{
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

public readonly record struct PaymentCardCardHolder
{
    public PaymentCardCardHolder(string value) => Value = ValueObjectValidation.Required(value, nameof(PaymentCardCardHolder), 100);
    public string Value { get; }
}

public readonly record struct PaymentCardAuthorizationCode
{
    public PaymentCardAuthorizationCode(string? value) => Value = ValueObjectValidation.Optional(value, nameof(PaymentCardAuthorizationCode), 100);
    public string? Value { get; }
}
