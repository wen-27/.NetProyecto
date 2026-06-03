// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PartPurchaseDetailValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.PartPurchaseDetail;

public readonly record struct PartPurchaseDetailPartPurchaseId
{
    public PartPurchaseDetailPartPurchaseId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PartPurchaseDetailPartPurchaseId));
    public int Value { get; }
}

public readonly record struct PartPurchaseDetailPartId
{
    public PartPurchaseDetailPartId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PartPurchaseDetailPartId));
    public int Value { get; }
}

public readonly record struct PartPurchaseDetailQuantity
{
    public PartPurchaseDetailQuantity(int value) => Value = ValueObjectValidation.Positive(value, nameof(PartPurchaseDetailQuantity));
    public int Value { get; }
}

public readonly record struct PartPurchaseDetailUnitPrice
{
    public PartPurchaseDetailUnitPrice(decimal value) => Value = ValueObjectValidation.Money(value, nameof(PartPurchaseDetailUnitPrice));
    public decimal Value { get; }
}
