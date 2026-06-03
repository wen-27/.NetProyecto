using Domain.ValueObjects;

namespace Domain.ValueObjects.Payment;

// Value Object que encapsula y valida un valor especifico de PaymentInvoiceId.
public readonly record struct PaymentInvoiceId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PaymentInvoiceId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PaymentInvoiceId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PaymentMethodId.
public readonly record struct PaymentMethodId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PaymentMethodId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PaymentMethodId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PaymentStatusId.
public readonly record struct PaymentStatusId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PaymentStatusId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PaymentStatusId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PaymentDate.
public readonly record struct PaymentDate
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PaymentDate(DateTime value) => Value = value == default ? throw new ArgumentException("La fecha de pago es obligatoria.", nameof(value)) : value;
    public DateTime Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PaymentAmount.
public readonly record struct PaymentAmount
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PaymentAmount(decimal value) => Value = ValueObjectValidation.Money(value, nameof(PaymentAmount));
    public decimal Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PaymentReference.
public readonly record struct PaymentReference
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PaymentReference(string? value) => Value = ValueObjectValidation.Optional(value, nameof(PaymentReference), 100);
    public string? Value { get; }
}
