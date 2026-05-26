using Domain.ValueObjects;

namespace Domain.ValueObjects.Customer;

public readonly record struct CustomerPersonId
{
    public CustomerPersonId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(CustomerPersonId));
    }
    public int Value { get; }
}
