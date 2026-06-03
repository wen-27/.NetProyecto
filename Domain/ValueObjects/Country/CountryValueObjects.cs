// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de CountryValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
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
