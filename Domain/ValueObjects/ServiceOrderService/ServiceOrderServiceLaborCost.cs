using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrderService;

public readonly record struct ServiceOrderServiceLaborCost
{
    public ServiceOrderServiceLaborCost(decimal value)
    {
        Value = ValueObjectValidation.Money(value, nameof(ServiceOrderServiceLaborCost));
    }

    public decimal Value { get; }
}
