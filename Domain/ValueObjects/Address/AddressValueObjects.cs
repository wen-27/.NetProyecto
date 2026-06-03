using Domain.ValueObjects;

namespace Domain.ValueObjects.Address;

// Value Object que encapsula y valida un valor especifico de AddressNeighborhoodId.
public readonly record struct AddressNeighborhoodId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public AddressNeighborhoodId(int value) => Value = ValueObjectValidation.Positive(value, nameof(AddressNeighborhoodId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de AddressStreetTypeId.
public readonly record struct AddressStreetTypeId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public AddressStreetTypeId(int value) => Value = ValueObjectValidation.Positive(value, nameof(AddressStreetTypeId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de AddressMainNumber.
public readonly record struct AddressMainNumber
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public AddressMainNumber(string? value) => Value = ValueObjectValidation.Optional(value, nameof(AddressMainNumber), 10);
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de AddressSecondaryNumber.
public readonly record struct AddressSecondaryNumber
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public AddressSecondaryNumber(string? value) => Value = ValueObjectValidation.Optional(value, nameof(AddressSecondaryNumber), 10);
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de AddressTertiaryNumber.
public readonly record struct AddressTertiaryNumber
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public AddressTertiaryNumber(string? value) => Value = ValueObjectValidation.Optional(value, nameof(AddressTertiaryNumber), 10);
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de AddressComplement.
public readonly record struct AddressComplement
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public AddressComplement(string? value) => Value = ValueObjectValidation.Optional(value, nameof(AddressComplement), 150);
    public string? Value { get; }
}
