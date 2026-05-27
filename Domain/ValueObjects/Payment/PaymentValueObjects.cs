using Domain.ValueObjects;

namespace Domain.ValueObjects.Payment;

public readonly record struct PaymentInvoiceId
{
    public PaymentInvoiceId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PaymentInvoiceId));
    public int Value { get; }
}

public readonly record struct PaymentMethodId
{
    public PaymentMethodId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PaymentMethodId));
    public int Value { get; }
}

public readonly record struct PaymentStatusId
{
    public PaymentStatusId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PaymentStatusId));
    public int Value { get; }
}

public readonly record struct PaymentDate
{
    public PaymentDate(DateTime value) => Value = value == default ? throw new ArgumentException("La fecha de pago es obligatoria.", nameof(value)) : value;
    public DateTime Value { get; }
}

public readonly record struct PaymentAmount
{
    public PaymentAmount(decimal value) => Value = ValueObjectValidation.Money(value, nameof(PaymentAmount));
    public decimal Value { get; }
}

public readonly record struct PaymentReference
{
    public PaymentReference(string? value) => Value = ValueObjectValidation.Optional(value, nameof(PaymentReference), 100);
    public string? Value { get; }
}
