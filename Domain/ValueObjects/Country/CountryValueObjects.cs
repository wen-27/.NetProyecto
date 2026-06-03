using Domain.ValueObjects;

namespace Domain.ValueObjects.Country;

// Value Object que encapsula y valida un valor especifico de CountryName.
public readonly record struct CountryName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public CountryName(string value) => Value = ValueObjectValidation.Required(value, nameof(CountryName), 100);
    public string Value { get; }
}

// Value Object que encapsula y valida un valor especifico de CountryPhoneCode.
public readonly record struct CountryPhoneCode
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public CountryPhoneCode(string? value) => Value = ValueObjectValidation.Optional(value, nameof(CountryPhoneCode), 10);
    public string? Value { get; }
}
