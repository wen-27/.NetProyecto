using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceType;

public readonly record struct ServiceTypeBasePrice
{
    public decimal Amount { get; }
    public decimal Value => Amount;

    public ServiceTypeBasePrice(decimal amount)
    {
        Amount = ValueObjectValidation.Money(amount, nameof(ServiceTypeBasePrice));
    }

    public static ServiceTypeBasePrice Zero() => new(0);

    public ServiceTypeBasePrice Add(ServiceTypeBasePrice other) => new(Amount + other.Amount);

    public ServiceTypeBasePrice Multiply(int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "La cantidad no puede ser negativa.");
        }

        return new ServiceTypeBasePrice(Amount * quantity);
    }

    public override string ToString() => Amount.ToString("F2");
}
