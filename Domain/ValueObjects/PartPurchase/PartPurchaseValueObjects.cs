using Domain.ValueObjects;

namespace Domain.ValueObjects.PartPurchase;

// Value Object que encapsula y valida un valor especifico de PartPurchaseSupplierId.
public readonly record struct PartPurchaseSupplierId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PartPurchaseSupplierId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PartPurchaseSupplierId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PartPurchasePurchaseDate.
public readonly record struct PartPurchasePurchaseDate
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PartPurchasePurchaseDate(DateTime value) => Value = value == default ? throw new ArgumentException("La fecha de compra es obligatoria.", nameof(value)) : value;
    public DateTime Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PartPurchaseTotal.
public readonly record struct PartPurchaseTotal
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PartPurchaseTotal(decimal value) => Value = ValueObjectValidation.Money(value, nameof(PartPurchaseTotal));
    public decimal Value { get; }
}
