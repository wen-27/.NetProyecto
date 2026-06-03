using Domain.ValueObjects;

namespace Domain.ValueObjects.OrderServicePart;

// Value Object que encapsula y valida un valor especifico de OrderServicePartOrderServiceId.
public readonly record struct OrderServicePartOrderServiceId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderServicePartOrderServiceId(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderServicePartOrderServiceId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de OrderServicePartPartId.
public readonly record struct OrderServicePartPartId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderServicePartPartId(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderServicePartPartId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de OrderServicePartQuantity.
public readonly record struct OrderServicePartQuantity
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderServicePartQuantity(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderServicePartQuantity));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de OrderServicePartAppliedUnitPrice.
public readonly record struct OrderServicePartAppliedUnitPrice
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderServicePartAppliedUnitPrice(decimal value) => Value = ValueObjectValidation.Money(value, nameof(OrderServicePartAppliedUnitPrice));
    public decimal Value { get; }
}

// Value Object que encapsula y valida un valor especifico de OrderServicePartCustomerApproved.
public readonly record struct OrderServicePartCustomerApproved
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderServicePartCustomerApproved(bool? value) => Value = value;
    public bool? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de OrderServicePartApprovalDate.
public readonly record struct OrderServicePartApprovalDate
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderServicePartApprovalDate(DateTime? value) => Value = value;
    public DateTime? Value { get; }
}
