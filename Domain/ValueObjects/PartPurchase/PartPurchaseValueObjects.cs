using Domain.ValueObjects;

namespace Domain.ValueObjects.PartPurchase;

public readonly record struct PartPurchaseSupplierId
{
    public PartPurchaseSupplierId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PartPurchaseSupplierId));
    public int Value { get; }
}

public readonly record struct PartPurchasePurchaseDate
{
    public PartPurchasePurchaseDate(DateTime value) => Value = value == default ? throw new ArgumentException("La fecha de compra es obligatoria.", nameof(value)) : value;
    public DateTime Value { get; }
}

public readonly record struct PartPurchaseTotal
{
    public PartPurchaseTotal(decimal value) => Value = ValueObjectValidation.Money(value, nameof(PartPurchaseTotal));
    public decimal Value { get; }
}
