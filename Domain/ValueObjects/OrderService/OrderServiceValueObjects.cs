using Domain.ValueObjects;

namespace Domain.ValueObjects.OrderService;

public readonly record struct OrderServiceServiceOrderId
{
    public OrderServiceServiceOrderId(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderServiceServiceOrderId));
    public int Value { get; }
}

public readonly record struct OrderServiceServiceTypeId
{
    public OrderServiceServiceTypeId(int value) => Value = ValueObjectValidation.Positive(value, nameof(OrderServiceServiceTypeId));
    public int Value { get; }
}

public readonly record struct OrderServiceDescription
{
    public OrderServiceDescription(string? value) => Value = ValueObjectValidation.Optional(value, nameof(OrderServiceDescription), 500);
    public string? Value { get; }
}

public readonly record struct OrderServiceWorkPerformed
{
    public OrderServiceWorkPerformed(string? value) => Value = ValueObjectValidation.Optional(value, nameof(OrderServiceWorkPerformed), 1000);
    public string? Value { get; }
}

public readonly record struct OrderServiceLaborCost
{
    public OrderServiceLaborCost(decimal value) => Value = ValueObjectValidation.Money(value, nameof(OrderServiceLaborCost));
    public decimal Value { get; }
}

public readonly record struct OrderServiceCustomerApproved
{
    public OrderServiceCustomerApproved(bool? value) => Value = value;
    public bool? Value { get; }
}

public readonly record struct OrderServiceApprovalDate
{
    public OrderServiceApprovalDate(DateTime? value) => Value = value;
    public DateTime? Value { get; }
}
