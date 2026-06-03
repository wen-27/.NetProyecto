// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de OrderServicePartValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.OrderServicePart;

public readonly record struct OrderServicePartOrderServiceId
{
    public OrderServicePartOrderServiceId(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderServicePartOrderServiceId));
    public int Value { get; }
}

public readonly record struct OrderServicePartPartId
{
    public OrderServicePartPartId(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderServicePartPartId));
    public int Value { get; }
}

public readonly record struct OrderServicePartQuantity
{
    public OrderServicePartQuantity(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderServicePartQuantity));
    public int Value { get; }
}

public readonly record struct OrderServicePartAppliedUnitPrice
{
    public OrderServicePartAppliedUnitPrice(decimal value) => Value = ValueObjectValidation.Money(value, nameof(OrderServicePartAppliedUnitPrice));
    public decimal Value { get; }
}

public readonly record struct OrderServicePartCustomerApproved
{
    public OrderServicePartCustomerApproved(bool? value) => Value = value;
    public bool? Value { get; }
}

public readonly record struct OrderServicePartApprovalDate
{
    public OrderServicePartApprovalDate(DateTime? value) => Value = value;
    public DateTime? Value { get; }
}
