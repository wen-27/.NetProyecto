using Domain.ValueObjects;

namespace Domain.ValueObjects.Country;

public readonly record struct CountryName
{
    public CountryName(string value) => Value = ValueObjectValidation.Required(value, nameof(CountryName), 100);
    public string Value { get; }
}

public readonly record struct CountryPhoneCode
{
    public CountryPhoneCode(string? value) => Value = ValueObjectValidation.Optional(value, nameof(CountryPhoneCode), 10);
    public string? Value { get; }
}
