using Domain.ValueObjects;

namespace Domain.ValueObjects.PartPurchaseDetail;

// Value Object que encapsula y valida un valor especifico de PartPurchaseDetailPartPurchaseId.
public readonly record struct PartPurchaseDetailPartPurchaseId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PartPurchaseDetailPartPurchaseId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PartPurchaseDetailPartPurchaseId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PartPurchaseDetailPartId.
public readonly record struct PartPurchaseDetailPartId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PartPurchaseDetailPartId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PartPurchaseDetailPartId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PartPurchaseDetailQuantity.
public readonly record struct PartPurchaseDetailQuantity
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PartPurchaseDetailQuantity(int value) => Value = ValueObjectValidation.Positive(value, nameof(PartPurchaseDetailQuantity));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PartPurchaseDetailUnitPrice.
public readonly record struct PartPurchaseDetailUnitPrice
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PartPurchaseDetailUnitPrice(decimal value) => Value = ValueObjectValidation.Money(value, nameof(PartPurchaseDetailUnitPrice));
    public decimal Value { get; }
}
