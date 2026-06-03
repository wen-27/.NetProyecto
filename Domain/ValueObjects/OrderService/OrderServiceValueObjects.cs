using Domain.ValueObjects;

namespace Domain.ValueObjects.OrderService;

// Value Object que encapsula y valida un valor especifico de OrderServiceServiceOrderId.
public readonly record struct OrderServiceServiceOrderId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderServiceServiceOrderId(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderServiceServiceOrderId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de OrderServiceServiceTypeId.
public readonly record struct OrderServiceServiceTypeId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderServiceServiceTypeId(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderServiceServiceTypeId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de OrderServiceDescription.
public readonly record struct OrderServiceDescription
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderServiceDescription(string? value) => Value = ValueObjectValidation.Optional(value, nameof(OrderServiceDescription), 500);
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de OrderServiceWorkPerformed.
public readonly record struct OrderServiceWorkPerformed
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderServiceWorkPerformed(string? value) => Value = ValueObjectValidation.Optional(value, nameof(OrderServiceWorkPerformed), 1000);
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de OrderServiceLaborCost.
public readonly record struct OrderServiceLaborCost
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderServiceLaborCost(decimal value) => Value = ValueObjectValidation.Money(value, nameof(OrderServiceLaborCost));
    public decimal Value { get; }
}

// Value Object que encapsula y valida un valor especifico de OrderServiceCustomerApproved.
public readonly record struct OrderServiceCustomerApproved
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderServiceCustomerApproved(bool? value) => Value = value;
    public bool? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de OrderServiceApprovalDate.
public readonly record struct OrderServiceApprovalDate
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public OrderServiceApprovalDate(DateTime? value) => Value = value;
    public DateTime? Value { get; }
}
