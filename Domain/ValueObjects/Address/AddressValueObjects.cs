using Domain.ValueObjects;

namespace Domain.ValueObjects.Address;

public readonly record struct AddressNeighborhoodId
{
    public AddressNeighborhoodId(int value) => Value = ValueObjectValidation.Positive(value, nameof(AddressNeighborhoodId));
    public int Value { get; }
}

public readonly record struct AddressStreetTypeId
{
    public AddressStreetTypeId(int value) => Value = ValueObjectValidation.Positive(value, nameof(AddressStreetTypeId));
    public int Value { get; }
}

public readonly record struct AddressMainNumber
{
    public AddressMainNumber(string? value) => Value = ValueObjectValidation.Optional(value, nameof(AddressMainNumber), 10);
    public string? Value { get; }
}

public readonly record struct AddressSecondaryNumber
{
    public AddressSecondaryNumber(string? value) => Value = ValueObjectValidation.Optional(value, nameof(AddressSecondaryNumber), 10);
    public string? Value { get; }
}

public readonly record struct AddressTertiaryNumber
{
    public AddressTertiaryNumber(string? value) => Value = ValueObjectValidation.Optional(value, nameof(AddressTertiaryNumber), 10);
    public string? Value { get; }
}

public readonly record struct AddressComplement
{
    public AddressComplement(string? value) => Value = ValueObjectValidation.Optional(value, nameof(AddressComplement), 150);
    public string? Value { get; }
}
